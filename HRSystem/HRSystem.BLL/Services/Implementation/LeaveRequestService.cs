using Azure.Core;
using HRSystem.BLL.ModelVM.LeaveRequest;
using HRSystem.DAL.Enums;
using HRSystem.DAL.Repository.Implementation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.BLL.Services.Implementation
{
    internal class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly UserManager<HRSystemUser> _userManager;

        public LeaveRequestService(
            ILeaveRequestRepository leaveRequestRepository,
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            UserManager<HRSystemUser> userManager)
        { 
            _leaveRequestRepository  = leaveRequestRepository;
            _employeeRepository= employeeRepository;
            _emailService= emailService;
            _userManager= userManager;
            _unitOfWork= unitOfWork;

        }

        /* ---------------------------------------------------------------------------------- */
        // For Employee

        public async Task<Response<IEnumerable<LeaveRequestVM>>> GetMyRequestsAsync(string userId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByUserIdAsync(userId);
                if (employee == null)
                    return new Response<IEnumerable<LeaveRequestVM>>(null, "Employee not found", false);

                var requests = await _leaveRequestRepository.GetByEmployeeIdAsync(employee.Id);

                var mapp = requests.Select(r => MapToVM(r));
                return new Response<IEnumerable<LeaveRequestVM>>(mapp, null, true);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<LeaveRequestVM>>(null, ex.Message, false);

            }

        }

        //Employee => Send Request
        public async Task<Response<bool>> SubmitAsync(LeaveRequestFormVM vm, string userId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByUserIdAsync(userId);
                if (employee == null)
                    return new Response<bool>(false, "Employee not found", false);

                // محتاجين بقي نحدد مين الشخص اللي هيعمل approve
                /*
                     * if employee has a supervisor => approve
                     * else HR approves
                */
                Guid? approverId = null;
                if (employee.SupervisorId.HasValue)
                {
                    approverId = employee.SupervisorId;

                }
                else
                {
                    // may be we have many HRs We need only one
                    var hrUsers = await _userManager.GetUsersInRoleAsync("HR");
                    var hrUser = hrUsers.FirstOrDefault();
                    if (hrUser != null)
                    {
                        var hrEmployee = await _employeeRepository
                            .GetEmployeeByUserIdAsync(hrUser.Id);
                        approverId = hrEmployee?.Id;
                    }
                }

                // prepare request 
                var request = new LeaveRequest
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = vm.LeaveTypeId,
                    FromDate = vm.FromDate,
                    ToDate = vm.ToDate,
                    Reason = vm.Reason,
                    Status = LeaveStatus.Pending,
                    ApproverId = approverId
                };

                await _leaveRequestRepository.AddAsync(request);
                await _unitOfWork.SaveChangesAsync();


                // Notify approver by email
                if (approverId.HasValue)
                {
                    var approver = await _employeeRepository
                        .GetEmployeeByIdWithDetailsAsync(approverId.Value);

                    if (approver != null)
                    {
                        await _emailService.SendAsync(
                            approver.User.Email!,
                            "New Leave Request Pending Your Approval",
                            $@"<p>Hi <b>{approver.User.FullName}</b>,</p>
                               <p><b>{employee.User.FullName}</b> has submitted a leave request 
                               from <b>{vm.FromDate:dd MMM yyyy}</b> 
                               to <b>{vm.ToDate:dd MMM yyyy}</b>.</p>
                               <p>Please log in to review it.</p>"
                        );
                    }
                }

                return new Response<bool>(true, null, true);

            }
            catch (Exception ex) 
            {
                return new Response<bool>(false, ex.Message, false);

            }

        }

        // Employee => Cancel Request
        public async Task<Response<bool>> CancelAsync(Guid id, string userId)
        {
            try
            {
                var request = await _leaveRequestRepository.GetByIdWithDetailsAsync(id);
                if (request == null)
                    return new Response<bool>(false, "Request not found", false);

                // only the owner can cancel
                if (request.Employee.UserId != userId)
                    return new Response<bool>(false, "Unauthorized", false);

                // can only cancel if still pending
                if (request.Status != LeaveStatus.Pending)
                    return new Response<bool>(false, "Only pending requests can be cancelled", false);

                request.Status = LeaveStatus.Cancelled;
                _leaveRequestRepository.Update(request);
                await _unitOfWork.SaveChangesAsync();


                return new Response<bool>(true, null, true);

            }
            catch (Exception ex) {
                return new Response<bool>(false, ex.Message, false);

            }
        }

        /* ---------------------------------------------------------------------------------- */

        // Approver => get pending requests
        public async Task<Response<IEnumerable<LeaveRequestVM>>> GetPendingForApproverAsync(string userId)
        {
            try
            {
                var approver = await _employeeRepository.GetEmployeeByUserIdAsync(userId);
                if (approver == null)
                    return new Response<IEnumerable<LeaveRequestVM>>(null, "Not found", false);

                var requests = await _leaveRequestRepository
                    .GetPendingForApproverAsync(approver.Id);


                var mapp = requests.Select(r => MapToVM(r));

                return new Response<IEnumerable<LeaveRequestVM>>(mapp, null, true);

            }
            catch (Exception ex) 
            {
                return new Response<IEnumerable<LeaveRequestVM>>(null, ex.Message, false);

            }
        }

        public async Task<Response<bool>> ProcessApprovalAsync(ApprovalVM vm, string userId)
        {
            try
            {
                var request = await _leaveRequestRepository
                   .GetByIdWithDetailsAsync(vm.LeaveRequestId);

                if (request == null)
                    return new Response<bool>(false, "Request not found", false);


                // verify this user is the assigned approver
                if (request.Approver?.UserId != userId)
                    return new Response<bool>(false, "Unauthorized", false);

                // only pending requests can be processed
                if (request.Status != LeaveStatus.Pending)
                    return new Response<bool>(false, "Request already processed", false);

                request.Status = vm.Decision;
                request.ApproverNote = vm.Note;

                _leaveRequestRepository.Update(request);
                await _unitOfWork.SaveChangesAsync();

                // Notify employee by email
                var statusText = vm.Decision == LeaveStatus.Approved ? "Approved" : "Rejected";

                await _emailService.SendAsync(
                    request.Employee.User.Email!,
                    $"Your Leave Request has been {statusText}",
                    $@"<p>Hi <b>{request.Employee.User.FullName}</b>,</p>
                       <p>Your leave request from <b>{request.FromDate:dd MMM yyyy}</b> 
                       to <b>{request.ToDate:dd MMM yyyy}</b> 
                       has been <b>{statusText}</b>.</p>
                       {(string.IsNullOrEmpty(vm.Note) ? "" : $"<p>Note: {vm.Note}</p>")}"
                );


                return new Response<bool>(true, null, true);

            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);

            }
        }

        // ── Helper ────────────────────────────────────────────────
        private static LeaveRequestVM MapToVM(LeaveRequest r) => new()
        {
            Id = r.Id,
            EmployeeName = r.Employee.User.FullName,
            LeaveTypeName = r.LeaveType.Name,
            FromDate = r.FromDate,
            ToDate = r.ToDate,
            TotalDays = (r.ToDate - r.FromDate).Days + 1,
            Reason = r.Reason,
            Status = r.Status,
            ApproverName = r.Approver?.User.FullName,
            ApproverNote = r.ApproverNote,
            CreatedAt = r.CreatedAt
        };
    }
}
