
using HRSystem.DAL.Enums;

namespace HRSystem.DAL.Entities
{
    public class LeaveRequest: SoftDeletableEntity
    {
       public string? Reason { get; set; } 
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending; // Default value is Pending
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        // Relationship With Employee
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
