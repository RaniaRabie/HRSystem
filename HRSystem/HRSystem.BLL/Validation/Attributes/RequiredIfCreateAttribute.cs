

namespace HRSystem.BLL.Validation.Attributes
{
    internal class RequiredIfCreateAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Get the Id property from the VM

            var idProperty = validationContext.ObjectType.GetProperty("Id");
            var idValue = idProperty?.GetValue(validationContext.ObjectInstance);

            // If Id is null → this is a Create operation → password is required
            bool isCreate = idValue is null || (Guid?)idValue == null;

            if (isCreate && string.IsNullOrWhiteSpace(value?.ToString()))
            {
                return new ValidationResult(ErrorMessage ?? "Password is required when creating a new employee");
            }

            return ValidationResult.Success;
        }
    }
}
