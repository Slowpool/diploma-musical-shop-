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
public class CartService(IGetGoodsUnitsRelatedToSaleService service) : ICartService
{
    public async Task<string> MoveGoodsBackToCart(Guid saleId)
    {
#warning hmm. one line, but um... one tough-readable line
        return string.Join(GoodsIdSeparator, (await service.GetGoodsUnitsRelatedToSale(saleId)).Select(goodsUnit => new { goodsUnit.GoodsId, TypeName = goodsUnit.GetType().Name }));
        
    }
}
