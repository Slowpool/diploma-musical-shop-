using DataLayer.Common;
using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.GoodsServices;
public interface IUpdateGoodsStatusService
{
    Task<Guid> UpdateGoodsStatus(Guid goodsId, Type goodsType, GoodsStatus status);
}
public class UpdateGoodsStatusService(IGetGoodsService getGoodsService, MusicalShopDbContext context) : IUpdateGoodsStatusService
{
    public async Task<Guid> UpdateGoodsStatus(Guid goodsId, Type goodsType, GoodsStatus status)
    {
        Goods goods = await getGoodsService.GetGoodsInfo(goodsId.ToString(), goodsType);
        goods.Status = status;
        context.Update(goods);
        context.SaveChanges();
        return goods.GoodsId;
    }
}
