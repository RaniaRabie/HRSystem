using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Repository.Abstraction
{
    public interface ILeaveRequestRepository: IGenericRepository<LeaveRequest>
    {
        // Employee sees their own requests
        Task<IEnumerable<LeaveRequest>> GetByEmployeeIdAsync(Guid employeeId);

        // Supervisor/HR sees pending requests to approve
        Task<IEnumerable<LeaveRequest>> GetPendingForApproverAsync(Guid approverId);

        // Full details for single request
        Task<LeaveRequest?> GetByIdWithDetailsAsync(Guid id);
    }
}
