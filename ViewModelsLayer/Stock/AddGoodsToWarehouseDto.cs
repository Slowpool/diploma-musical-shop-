using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Stock;
public record class AddGoodsToWarehouseDto(string? GoodsName, KindOfGoods KindOfGoods, string? SpecificType, bool NewSpecificTypeIsBeingAdded, string? NewSpecificType, int? Price, GoodsStatus Status, string? Description, int? NumberOfUnits, GoodsKindSpecificDataDto? GoodsKindSpecificDataDto);
