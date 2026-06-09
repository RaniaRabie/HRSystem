using HRSystem.DAL.Models.Base;

namespace HRSystem.DAL.Models.Entities
{
    public class Employee : SoftDeletableEntity
    {

        public string FirstName { get; set; } = String.Empty; 
        public string LastName { get; set; } = String.Empty;

        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }

        // Relationship With Identity User
        public string UserId { get; set; } = string.Empty; // FK to Identity User
        public HRSystemUser User { get; set; } = null!;

        // Relationship With Department(Manager)
        public ICollection <Department> ManagedDepartments { get; set; } = new HashSet<Department>();

        // Relationship With Position
        public Guid PositionId { get; set; }
        public Position Position { get; set; } = null!;

        // Relationship With Supervisor (Self-Referencing)
        public Guid? SupervisorId { get; set; }
        public Employee? Supervisor { get; set; }

        // Relationship With Subordinates (Self-Referencing)
        public ICollection<Employee> Subordinates { get; set; } = new HashSet<Employee>();

        // Relationship With LeaveRequest( Leave Requests submitted by this employee)
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new HashSet<LeaveRequest>();

        // Relationship With LeaveRequest( Leave Requests this employee needs to approve (as Supervisor))
        public ICollection<LeaveRequest> LeaveRequestsToApprove { get; set; } = new HashSet<LeaveRequest>();

        // Relationship With Attendance
        public ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();

        // Relationship With Payroll
        public ICollection<Payroll> Payrolls { get; set; } = new HashSet<Payroll>();

    }
}
