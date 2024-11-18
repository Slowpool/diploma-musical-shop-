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
    string GoodsName,
    [RussianRequired("Вид")]
    KindOfGoods KindOfGoods,
    [RussianRequired("Тип")]
    string SpecificType,
    [RussianRequired("Выполнить добавление нового типа")]
    bool NewSpecificTypeIsBeingAdded,
    //[RussianRequired("Новый тип")]
    string? NewSpecificType,
    [RussianRequired("Цена")]
    // TODO to russian, check exclusion
    [Range(1, 2_147_483_647, MinimumIsExclusive = false, MaximumIsExclusive = false)]
    int? Price,
    [RussianRequired("Статус")]
    GoodsStatus Status,
    string? Description,
    [RussianRequired("Количество")]
    int? NumberOfUnits,
    [Required(ErrorMessage = "Отсутствуют специфичные данные")]
    GoodsKindSpecificDataDto GoodsKindSpecificDataDto);
