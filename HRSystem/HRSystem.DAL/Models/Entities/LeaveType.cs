using HRSystem.DAL.Models.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Models.Entities
{
    public class LeaveType: SoftDeletableEntity
    {
        public string Name { get; set; } = string.Empty;

        // Navigation
        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new HashSet<LeaveRequest>();
    }
}
