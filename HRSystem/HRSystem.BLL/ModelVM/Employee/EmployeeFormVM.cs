namespace HRSystem.BLL.ModelVM.Employee
{
    public class EmployeeFormVM
    {
        /* -------------------------------------------------------------------------------------- */

        public Guid? Id { get; set; }

        /* -------------------------------------------------------------------------------------- */

        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        [NoSpecialCharacters]
        public string FirstName { get; set; } = String.Empty;

        /* -------------------------------------------------------------------------------------- */


        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        [NoSpecialCharacters]
        public string LastName { get; set; } = String.Empty;

        /* -------------------------------------------------------------------------------------- */

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = String.Empty;

        /* -------------------------------------------------------------------------------------- */
        [RequiredIfCreate]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
        public string? Password { get; set; } // only on create  

        /* -------------------------------------------------------------------------------------- */

        [Range(0, 999999, ErrorMessage = "Salary must be a positive value")]
        public decimal Salary { get; set; }

        /* -------------------------------------------------------------------------------------- */
        [Required(ErrorMessage = "Hire date is required")]
        [NotDefaultDate]
        [FutureDateNotAllowed]
        public DateTime HireDate { get; set; }

        /* -------------------------------------------------------------------------------------- */

        [Required(ErrorMessage = "Position is required")]
        public Guid PositionId { get; set; }

        /* -------------------------------------------------------------------------------------- */

        public Guid? SupervisorId { get; set; }

        /* -------------------------------------------------------------------------------------- */

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; } = "Employee";
    }
}
