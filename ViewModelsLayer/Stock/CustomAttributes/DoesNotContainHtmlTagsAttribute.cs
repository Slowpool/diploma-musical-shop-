using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock.CustomAttributes;
[AttributeUsage(AttributeTargets.Class)]
public class DoesNotContainHtmlTagsAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var obj = validationContext.ObjectInstance;
        var objType = obj.GetType();
        foreach(var property in objType.GetProperties()
                                       .Where(property => property.PropertyType == typeof(string)))
        {
            var propertyValue = (string?)property.GetValue(obj);
            if (propertyValue is null)
                continue;
            var regexp = new Regex("<[^>]*>");
            if (regexp.IsMatch(propertyValue))
                return new ValidationResult(string.Format(CommonNames.HtmlTagsViolationMessageRu, property.Name));
        }
        return ValidationResult.Success;
    }
}
