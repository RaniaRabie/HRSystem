using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.BLL.ModelVM.LeaveType
{
    public class LeaveTypeFormVM
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Leave type name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters")]
        public string Name { get; set; } = string.Empty;
    }
}
