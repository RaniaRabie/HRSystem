namespace HRSystem.BLL.Validation.Attributes
{
    internal class NotDefaultDateAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(ErrorMessage ?? "Date is required");
            }

            if (value is DateTime date && date == DateTime.MinValue)
            {
                return new ValidationResult(ErrorMessage ?? "A valid date is required");
            }

            return ValidationResult.Success;
        }
    }
}
