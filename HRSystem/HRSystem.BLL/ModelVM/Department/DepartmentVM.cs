using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.BLL.ModelVM.Department
{
    public class DepartmentVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string MgrName { get; set; } = string.Empty;

        public int PositionsCount { get; set; }
        public int EmployeeCount { get; set; }
    }
}
