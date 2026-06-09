using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.BLL.ModelVM.Employee
{
    public class RecentEmployeeVM
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PositionName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
}
