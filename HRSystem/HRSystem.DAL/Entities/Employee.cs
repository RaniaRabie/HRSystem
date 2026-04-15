using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Entities
{
    public class Employee : SoftDeletableEntity
    {

        public string FirstName { get; set; } = null!; // null! means it is required and will not be null, but we are not initializing it here
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }

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

        // Relationship With LeaveRequest
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new HashSet<LeaveRequest>();

        // Relationship With Attendance
        public ICollection<Attendance> Attendances { get; set; } = new HashSet<Attendance>();

        // Relationship With Payroll
        public ICollection<Payroll> Payrolls { get; set; } = new HashSet<Payroll>();

    }
}
