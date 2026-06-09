using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.BLL.ModelVM.Employee
{
    public class EmployeeProfileVM
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PositionName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string? SupervisorName { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
    }
}
