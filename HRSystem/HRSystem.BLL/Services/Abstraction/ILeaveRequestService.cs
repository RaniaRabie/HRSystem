using HRSystem.BLL.ModelVM.LeaveRequest;


namespace HRSystem.BLL.Services.Abstraction
{
    public interface ILeaveRequestService
    {
        // Employee
        Task<Response<IEnumerable<LeaveRequestVM>>> GetMyRequestsAsync(string userId);
        Task<Response<bool>> SubmitAsync(LeaveRequestFormVM vm, string userId);
        Task<Response<bool>> CancelAsync(Guid id, string userId);

        // Supervisor / HR
        Task<Response<IEnumerable<LeaveRequestVM>>> GetPendingForApproverAsync(string userId);
        Task<Response<bool>> ProcessApprovalAsync(ApprovalVM vm, string userId);
    }
}
