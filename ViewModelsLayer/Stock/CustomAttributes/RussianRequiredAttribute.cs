using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock.CustomAttributes;
[AttributeUsage(AttributeTargets.Parameter)]
public class RussianRequiredAttribute : RequiredAttribute
{
    public RussianRequiredAttribute(string propertyName)
    {
        ErrorMessage = string.Format(CommonNames.EmptyFieldRu, propertyName);
    }

    public override bool IsValid(object? value)
        => value is not null;

}
