using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Goods;
public record class GoodsUnitModel(Guid Guid, KindOfGoods KindOfGoods, string Name, int Price, GoodsStatus Status, string? Description);
