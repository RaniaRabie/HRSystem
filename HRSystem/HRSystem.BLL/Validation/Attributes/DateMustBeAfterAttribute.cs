using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.BLL.Validation.Attributes
{
    internal class DateMustBeAfterAttribute: ValidationAttribute
    {

        private readonly string _otherPropertyName;

        public DateMustBeAfterAttribute(string otherPropertyName)
        {
            _otherPropertyName = otherPropertyName;
            ErrorMessage = $"End date must be after start date";
        }

        protected override ValidationResult? IsValid(
            object? value, ValidationContext context)
        {
            if (value is not DateTime toDate)
                return ValidationResult.Success;

            var otherProperty = context.ObjectType.GetProperty(_otherPropertyName);
            if (otherProperty == null)
                return ValidationResult.Success;

            var fromDate = otherProperty.GetValue(context.ObjectInstance);
            if (fromDate is not DateTime from)
                return ValidationResult.Success;

            return toDate >= from
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage);
        }

    }
}
