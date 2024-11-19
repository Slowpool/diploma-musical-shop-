using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock.CustomAttributes;
public class RequiredWhenNewSpecificTypeIsTrueAttribute : RequiredAttribute
{
    public RequiredWhenNewSpecificTypeIsTrueAttribute(string propertyName)
    {
        ErrorMessage = string.Format(CommonNames.EmptyFieldRu, propertyName);
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        AddGoodsToWarehouseDto dto = (AddGoodsToWarehouseDto)validationContext.ObjectInstance;
        if (dto.NewSpecificTypeIsBeingAdded)
            return base.IsValid(value)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage)
    }
}
