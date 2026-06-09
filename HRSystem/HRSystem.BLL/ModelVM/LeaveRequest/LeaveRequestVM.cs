
using HRSystem.DAL.Enums;

namespace HRSystem.BLL.ModelVM.LeaveRequest
{
    public class LeaveRequestVM
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string LeaveTypeName { get; set; } = string.Empty;
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int TotalDays { get; set; }
        public string Reason { get; set; } = string.Empty;
        public LeaveStatus Status { get; set; }
        public string? ApproverName { get; set; }
        public string? ApproverNote { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
