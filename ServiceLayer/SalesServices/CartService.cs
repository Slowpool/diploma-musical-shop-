using DataLayer.Common;
using ServiceLayer.GoodsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.CommonNames;

namespace ServiceLayer.SalesServices;

public interface ICartService
{
    Task<string> MoveGoodsBackToCart(Guid saleId);
}
#warning so where is services for adding to and removing from the cart
public class CartService(MusicalShopDbContext context, IGetGoodsUnitsRelatedToSaleService service) : ICartService
{
    public async Task<string> MoveGoodsBackToCart(Guid saleId)
    {
        var goods = await service.GetOrigGoodsUnitsRelatedToSale(saleId);
        foreach (var goodsUnit in goods)
        {
            goodsUnit.Status = GoodsStatus.InStock;
#warning does this update works?
            context.Update(goodsUnit);
        }
        context.SaveChanges();

        return string.Join(GoodsIdSeparator, goods.Select(goodsUnit => new { goodsUnit.GoodsId, TypeName = goodsUnit.GetType().Name }));
        
    }
}
