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
    public RussianRequiredAttribute(string parameterName)
    {
        ErrorMessage = string.Format(CommonNames.FieldIsRequiredMessageRu, parameterName);
    }
    //public override bool IsValid(object? value)
        //=> value is not null;

}
