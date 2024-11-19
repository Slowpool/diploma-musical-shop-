using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock.CustomAttributes;
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
public class RequiredWhenKindOfGoodsIsAttribute : RequiredAttribute
{
    private readonly KindOfGoods targetKindOfGoods;
    public RequiredWhenKindOfGoodsIsAttribute(KindOfGoods kindOfGoods, string parameterName)
    {
        this.targetKindOfGoods = kindOfGoods;
        ErrorMessage = string.Format(CommonNames.FieldIsRequiredMessageRu, parameterName);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dto = (AddGoodsToWarehouseDto)validationContext.ObjectInstance;
        if (dto.KindOfGoods == targetKindOfGoods)
// TODO difference?
            return base.IsValid(value, validationContext);
        else
            return ValidationResult.Success;
    }
}
