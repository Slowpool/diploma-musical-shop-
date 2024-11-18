using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock;
[AttributeUsage(AttributeTargets.Parameter)]
public class RussianRequiredAttribute : ValidationAttribute
{
    public RussianRequiredAttribute(string propertyName)
    {
        ErrorMessage = string.Format(CommonNames.EmptyFieldRu, propertyName);
    }

    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        if (value is null)
            return new ValidationResult(ErrorMessage);
        else
            return ValidationResult.Success!;
    }

}
