using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.BLL.ModelVM.LeaveType
{
    public class LeaveTypeVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int RequestsCount { get; set; }
    }
}
