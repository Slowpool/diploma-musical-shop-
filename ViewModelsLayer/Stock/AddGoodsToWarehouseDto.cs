using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock;
public record class AddGoodsToWarehouseDto(
    [Required]
    string? GoodsName,
    [Required]
    KindOfGoods KindOfGoods,
    [Required]
    string? SpecificType,
    [Required]
    bool NewSpecificTypeIsBeingAdded,
    string? NewSpecificType,
    [Required]
    int? Price,
    [Required]
    GoodsStatus Status,
    string? Description,
    [Required]
    int? NumberOfUnits,
    [Required]
    GoodsKindSpecificDataDto? GoodsKindSpecificDataDto);
