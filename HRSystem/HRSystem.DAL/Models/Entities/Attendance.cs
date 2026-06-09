using HRSystem.DAL.Models.Base;

namespace HRSystem.DAL.Models.Entities
{
    public class Attendance:AuditableEntity
    {
        public DateTime? CheckIn { get; set; } // The time the employee checked in
        public DateTime? CheckOut { get; set; } // The time the employee checked out
        public DateTime Date { get; set; } // The date of the attendance record

        // Relationship With Employee
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
