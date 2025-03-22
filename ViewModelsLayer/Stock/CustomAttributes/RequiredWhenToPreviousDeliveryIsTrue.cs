using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock.CustomAttributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
public class RequiredWhenToPreviousDeliveryIsTrue : RequiredAttribute
{
    public RequiredWhenToPreviousDeliveryIsTrue(string parameterName)
    {
        ErrorMessage = string.Format(CommonNames.FieldIsRequiredMessageRu, parameterName);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dto = (AddGoodsToWarehouseDto)validationContext.ObjectInstance;
        if (dto.ToPreviousDelivery)
            // TODO difference?
            return base.IsValid(value, validationContext);
        else
            return ValidationResult.Success;
    }
}
