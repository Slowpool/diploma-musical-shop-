using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock;
public record class AddGoodsToWarehouseDto(
    // TODO sort out goodsName-must-be-not-null bug
    [RussianRequired("Наименование")]
    [StringLength(ConstValues.MaxGoodsNameLength)]
    string Name,

    [RussianRequired("Вид")]
    [Required()]
    KindOfGoods KindOfGoods,

    [RussianRequired("Тип")]
    string SpecificType,

    [RussianRequired("Выполнить добавление нового типа")]
    bool NewSpecificTypeIsBeingAdded,

    //[RussianRequired("Новый тип")]
    [RequiredWhenNewSpecificTypeIsTrue]
    string? NewSpecificType,

    [RussianRequired("Цена")]
    [Range(ConstValues.MinGoodsPriceValue, ConstValues.MaxPriceValue)]
    int? Price,

    [RussianRequired("Статус")]
    GoodsStatus Status,

    [StringLength(ConstValues.MaxGoodsDescriptionLength)]
    string? Description,

    [RussianRequired("Количество")]
    int? NumberOfUnits,

    [Required(ErrorMessage = "Отсутствуют специфичные данные")]
    GoodsKindSpecificDataDto GoodsKindSpecificDataDto);
