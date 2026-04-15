using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Entities
{
    public class Payroll:AuditableEntity
    {

        public decimal BaseSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }

        public decimal NetSalary { get; set; }

        public DateTime Month { get; set; }


        // Relationship With Employee
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = null!;
    }
}
