namespace HRSystem.BLL.Validation.Attributes
{
    internal class FutureDateNotAllowedAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage ?? "Date is required");
            }

            if (value is DateTime date && date > DateTime.UtcNow)
            {
                return new ValidationResult(ErrorMessage ?? "Hire date cannot be in the future");
            }

            return ValidationResult.Success;
        }
    }
}
