using DataLayer.Common;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.GoodsServices;
public interface IUpdateGoodsStatusService
{
    Task<Guid> UpdateGoodsStatus(Guid goodsId, KindOfGoods kindOfGoods, GoodsStatus status);
}
public class UpdateGoodsStatusService(IGetGoodsService getGoodsService, MusicalShopDbContext context) : IUpdateGoodsStatusService
{
    public async Task<Guid> UpdateGoodsStatus(Guid goodsId, KindOfGoods kindOfGoods, GoodsStatus status)
    {
        Goods goods = await getGoodsService.GetGoodsInfo(goodsId.ToString(), kindOfGoods);
        goods.Status = status;
        context.Update(goods);
        context.SaveChanges();
        return goods.GoodsId;
    }
}
