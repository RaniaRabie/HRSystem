using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.BLL.ModelVM.Dashboard
{
    public class DashboardVM
    {
        // Admin & HR only
        public int TotalEmployees { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalPositions { get; set; }
        public int ActiveUsers { get; set; }
        public IEnumerable<RecentEmployeeVM> RecentEmployees { get; set; }
            = new List<RecentEmployeeVM>();

        // Employee only
        public EmployeeProfileVM? MyProfile { get; set; }
    }
}
