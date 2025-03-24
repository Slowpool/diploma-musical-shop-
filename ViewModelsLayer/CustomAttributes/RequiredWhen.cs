using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.CustomAttributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
public class RequiredWhen : RequiredAttribute
{
    private readonly string _propertyName;
    private readonly object? _valueToBeRequired;
    public RequiredWhen(string propertyName, object? valueToBeRequired)
    {
        this._propertyName = propertyName;
        this._valueToBeRequired = valueToBeRequired;
        ErrorMessage = string.Format(CommonNames.FieldIsRequiredMessageRu, propertyName);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var @object = validationContext.ObjectInstance;
        var type = @object.GetType();
        var property = type.GetProperty(_propertyName)!;
        if (property.GetValue(@object) == _valueToBeRequired)
            return base.IsValid(value, validationContext);
        else
            return ValidationResult.Success;
    }
}
