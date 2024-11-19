using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock.CustomAttributes;
public class RequiredWhenNewSpecificTypeAddingIsEqualToAttribute : RequiredAttribute
{
    private readonly bool newSpecificTypeAdding;
    public RequiredWhenNewSpecificTypeAddingIsEqualToAttribute(bool newSpecificTypeAdding, string propertyName)
    {
        ErrorMessage = string.Format(CommonNames.FieldIsRequiredMessageRu, propertyName);
        this.newSpecificTypeAdding = newSpecificTypeAdding;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        AddGoodsToWarehouseDto dto = (AddGoodsToWarehouseDto)validationContext.ObjectInstance;
        if (dto.NewSpecificTypeIsBeingAdded == newSpecificTypeAdding)
            // default check
            // TODO difference?
            return base.IsValid(value)
                ? ValidationResult.Success
                : new ValidationResult(ErrorMessage);
        else
            // ignore
            return ValidationResult.Success;
    }
}
