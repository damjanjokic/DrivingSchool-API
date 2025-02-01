using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BuckleApp.Application.Valications;

public class DateTimeUtcAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var dateTime = (DateTime)value;

        return 
            dateTime.Kind != DateTimeKind.Utc ? new ValidationResult("Date has to be UTC") 
                : ValidationResult.Success;
    }
}