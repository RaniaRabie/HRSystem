

namespace HRSystem.BLL.ModelVM.LeaveRequest
{
    public class LeaveRequestFormVM
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "Please select a leave type")]
        public Guid LeaveTypeId { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [NotDefaultDate]
        [FutureDateNotAllowed]
        public DateTime FromDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [NotDefaultDate]
        [DateMustBeAfter("FromDate")]  // custom validation
        public DateTime ToDate { get; set; }

        [Required(ErrorMessage = "Reason is required")]
        [MaxLength(500, ErrorMessage = "Reason cannot exceed 500 characters")]
        [MinLength(10, ErrorMessage = "Please provide more details")]
        public string Reason { get; set; } = string.Empty;
    }
}
