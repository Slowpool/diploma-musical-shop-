using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Stock.CustomAttributes;

namespace ViewModelsLayer.Stock;
[DoesNotContainHtmlTags]
public record class AddGoodsToWarehouseDto(
    [RussianRequired("Наименование")]
    [RussianStringLength(ConstValues.GoodsNameMaxLength, "Наименование")]
    string Name,

    [RequiredWhenNewSpecificTypeAddingIsEqualTo(false, "Тип")]
    [RussianStringLength(ConstValues.GoodsSpecificTypeMaxLength, "Тип")]
    string? SpecificType,

    [RussianRequired("Выполнить добавление нового типа")]
    bool NewSpecificTypeIsBeingAdded,

    [RequiredWhenNewSpecificTypeAddingIsEqualTo(true, "Название нового типа")]
    [RussianStringLength(ConstValues.GoodsSpecificTypeMaxLength, "Название нового типа")]
    string? NewSpecificType,

    [RussianRequired("Цена")]
    [Range(ConstValues.GoodsPriceMinValue, ConstValues.GoodsPriceMaxValue, ErrorMessage = "Значение поля \"Цена\" должно быть в диапазоне от {1} до {2}")]
    int? Price,

    [RussianRequired("Статус")]
    GoodsStatus Status,

    [RussianStringLength(ConstValues.GoodsDescriptionMaxLength, "Описание")]
    string? Description,

    [RussianRequired("Количество")]
    [Range(ConstValues.GoodsPriceMinValue, ConstValues.GoodsPriceMaxValue, ErrorMessage = "Значение поля \"Количество\" должно быть в диапазоне от {1} до {2}")]
    int? NumberOfUnits,

    [Required(ErrorMessage = "Отсутствуют специфичные данные")]
    GoodsKindSpecificDataDto GoodsKindSpecificDataDto,

    [RequiredWhenToPreviousDeliveryIsTrue("Идентификатор текущей доставки ")]
    Guid? DeliveryId,
    [RussianRequired("Дополнить предыдущую доставку")]
    bool ToPreviousDelivery

);
