

using HRSystem.DAL.Enums;

namespace HRSystem.BLL.ModelVM.LeaveRequest
{
    public class ApprovalVM
    {
        public Guid LeaveRequestId { get; set; }

        [Required]
        public LeaveStatus Decision { get; set; } // Approved or Rejected

        [MaxLength(500)]
        public string? Note { get; set; }
    }
}
