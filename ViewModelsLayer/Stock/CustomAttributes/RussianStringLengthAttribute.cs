using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock.CustomAttributes;
[AttributeUsage(AttributeTargets.Parameter)]
public class RussianStringLengthAttribute : StringLengthAttribute
{
    public RussianStringLengthAttribute(int maximumLength, string fieldName) : base(maximumLength)
    {
        ErrorMessage = string.Format(CommonNames.MaxLengthViolationMessageRu, fieldName, maximumLength);
    }
}
