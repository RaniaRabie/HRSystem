using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.BLL.ModelVM.Position
{
    public class PositionVM
    {
        public Guid Id { get; set; }
        public string Name{ get; set; } 
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; } 
        public int EmployeesCount { get; set; }
    }
}
