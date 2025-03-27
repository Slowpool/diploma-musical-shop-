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
    [RussianStringLength(Consts.GoodsNameMaxLength, "Наименование")]
    string Name,

    [RequiredWhenNewSpecificTypeAddingIsEqualTo(false, "Тип")]
    [RussianStringLength(Consts.GoodsSpecificTypeMaxLength, "Тип")]
    string? SpecificType,

    [RussianRequired("Выполнить добавление нового типа")]
    bool NewSpecificTypeIsBeingAdded,

    [RequiredWhenNewSpecificTypeAddingIsEqualTo(true, "Название нового типа")]
    [RussianStringLength(Consts.GoodsSpecificTypeMaxLength, "Название нового типа")]
    string? NewSpecificType,

    [RussianRequired("Цена")]
    [Range(Consts.GoodsPriceMinValue, Consts.GoodsPriceMaxValue, ErrorMessage = "Значение поля \"Цена\" должно быть в диапазоне от {1} до {2}")]
    int? Price,

    [RussianRequired("Статус")]
    GoodsStatus Status,

    [RussianStringLength(Consts.GoodsDescriptionMaxLength, "Описание")]
    string? Description,

    [RussianRequired("Количество")]
    [Range(Consts.GoodsPriceMinValue, Consts.GoodsPriceMaxValue, ErrorMessage = "Значение поля \"Количество\" должно быть в диапазоне от {1} до {2}")]
    int? NumberOfUnits,

    [Required(ErrorMessage = "Отсутствуют специфичные данные")]
    GoodsKindSpecificDataDto GoodsKindSpecificDataDto,

    [RequiredWhenToPreviousDeliveryIsTrue("Идентификатор текущей доставки ")]
    Guid? DeliveryId,
    [RussianRequired("Дополнить предыдущую доставку")]
    bool ToPreviousDelivery

);
