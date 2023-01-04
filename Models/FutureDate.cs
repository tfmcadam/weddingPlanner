#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace weddingPlannerTwo.Models;

public class FutureDateAttribute : ValidationAttribute
{
    // Call upon the protected IsValid method
    protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("words");
        }
        if ((DateTime)value <= DateTime.Now)
        {
            return new ValidationResult("Date must be in the Future");
        }

        else
        {
            // Otherwise, we were successful and can report our success  
            return ValidationResult.Success;
        }

    }
}