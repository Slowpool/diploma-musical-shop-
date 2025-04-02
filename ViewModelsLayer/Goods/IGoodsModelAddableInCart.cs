using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModelsLayer.Goods;
public interface IGoodsModelAddableInCart
{
    Guid GoodsId { get; }
    GoodsStatus Status { get; }
}
