using HRSystem.BLL.ModelVM.LeaveRequest;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRSystem.MVC.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly ILeaveTypeService _leaveTypeService;

        public LeaveRequestController(
            ILeaveRequestService leaveRequestService,
            ILeaveTypeService leaveTypeService)
        {
            _leaveRequestService = leaveRequestService;
            _leaveTypeService = leaveTypeService;

        }

        /* -------------------------------------------------------------- */
        // My Requests — Employee sees own requests

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> MyRequests()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _leaveRequestService.GetMyRequestsAsync(userId);

            if (!response.IsSuccess)
            {
                TempData["Error"] = response.ErrorMessage;
                return View(Enumerable.Empty<LeaveRequestVM>());
            }

            return View(response.Value);
        }
        /* -------------------------------------------------------------- */
        // Submit Request

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Create()
        {
            await PopulateLeaveTypesDropdown();
            return View("Form", new LeaveRequestFormVM());
        }

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Create(LeaveRequestFormVM model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateLeaveTypesDropdown();
                return View("Form", model);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _leaveRequestService.SubmitAsync(model, userId);

            if (!response.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, response.ErrorMessage!);
                await PopulateLeaveTypesDropdown();
                return View("Form", model);
            }

            TempData["Success"] = "Leave request submitted successfully";
            return RedirectToAction(nameof(MyRequests));
        }

        /* -------------------------------------------------------------- */
        // Cancel Request

        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _leaveRequestService.CancelAsync(id, userId);

            TempData[response.IsSuccess ? "Success" : "Error"] = response.IsSuccess
                ? "Request cancelled successfully"
                : response.ErrorMessage;

            return RedirectToAction(nameof(MyRequests));
        }

        /* -------------------------------------------------------------- */
        // Pending Approvals — Supervisor / HR

        [Authorize(Roles = "Employee,HR,Admin")]
        public async Task<IActionResult> PendingApprovals()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _leaveRequestService.GetPendingForApproverAsync(userId);

            if (!response.IsSuccess)
            {
                TempData["Error"] = response.ErrorMessage;
                return View(Enumerable.Empty<LeaveRequestVM>());
            }

            return View(response.Value);
        }

        /* -------------------------------------------------------------- */
        // Process Approval

        [HttpPost]
        [Authorize(Roles = "Employee,HR,Admin")]
        public async Task<IActionResult> ProcessApproval(ApprovalVM model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var response = await _leaveRequestService.ProcessApprovalAsync(model, userId);

            TempData[response.IsSuccess ? "Success" : "Error"] = response.IsSuccess
                ? "Decision submitted successfully"
                : response.ErrorMessage;

            return RedirectToAction(nameof(PendingApprovals));
        }

        /* -------------------------------------------------------------- */
        // Dropdown Helper

        private async Task PopulateLeaveTypesDropdown()
        {
            var response = await _leaveTypeService.GetAllAsync();
            ViewBag.LeaveTypes = response.Value?.Select(lt => new
            {
                lt.Id,
                lt.Name
            }) ?? Enumerable.Empty<object>();
        }
    }
}
