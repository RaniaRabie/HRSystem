using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace HRSystem.BLL.Validation.Attributes
{
    internal class NoSpecialCharactersAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if(value is not string input)
                return new ValidationResult("Invalid input type");

            if (Regex.IsMatch(input, @"^[\p{L}\s\-]+$"))
                return ValidationResult.Success;
            
            return new ValidationResult(ErrorMessage ?? "Only letters, spaces, and hyphens are allowed");


        }
    }
}
