using HRSystem.DAL.Models.Base;

namespace HRSystem.DAL.Models.Entities
{
    public class LeaveRequest: SoftDeletableEntity
    {
       public string? Reason { get; set; } 
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending; // Default value is Pending
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string? ApproverNote { get; set; }


        // Relationship With Employee
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;

        public Guid LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; } = null!;

        public Guid? ApproverId { get; set; }
        public Employee? Approver { get; set; }

    }
}
