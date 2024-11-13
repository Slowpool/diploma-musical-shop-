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
    Task<string?> AddToOrRemoveFromCart(string goodsId, bool isInCart, string? goodsIdsAndKinds);

}
#warning so where is services for adding to and removing from the cart
#warning UPD: what did i mean?
public class CartService(MusicalShopDbContext context, IGetGoodsUnitsRelatedToSaleService goodsRelatedToSaleService, IGetGoodsService getGoodsService, IUpdateGoodsStatusService updateGoodsStatusService) : ICartService
{
    public async Task<string> MoveGoodsBackToCart(Guid saleId)
    {
        var goods = await goodsRelatedToSaleService.GetOrigGoodsUnitsRelatedToSale(saleId);
        foreach (var goodsUnit in goods)
        {
            goodsUnit.Status = GoodsStatus.InStock;
#warning does this update works?
            context.Update(goodsUnit);
        }
        context.SaveChanges();

        // TODO change to kind of goods
        return string.Join(GoodsIdSeparator, goods.Select(goodsUnit => new { goodsUnit.GoodsId, TypeName = goodsUnit.GetType().Name }));
        
    }

    // dirty stuff
    public async Task<string?> AddToOrRemoveFromCart(string goodsId, bool isInCart, string? goodsIdsAndTypes)
    {
        List<string> goodsIdsTypesList = goodsIdsAndTypes?.Split(GoodsIdSeparator, StringSplitOptions.RemoveEmptyEntries)
                                                         ?.ToList() ?? [];
        var goodsType = await getGoodsService.GetGoodsType(goodsId);
        var goods = await getGoodsService.GetGoodsInfo(goodsId, goodsType);
        List<string> updatedGoodsIdsList = isInCart ? RemoveFromCart(goodsId, goodsIdsTypesList) : await AddInCart(goodsId, goodsType.Name, goodsIdsTypesList);
        await updateGoodsStatusService.UpdateGoodsStatus(goods.GoodsId, goodsType, isInCart ? GoodsStatus.InStock : GoodsStatus.InCart);
        if (updatedGoodsIdsList == null)
            return null;
        else
            return string.Join(GoodsIdSeparator, updatedGoodsIdsList);
    }

    private List<string> RemoveFromCart(string goodsId, List<string> goodsIdsTypesList)
    {
        foreach (var goodsIdType in goodsIdsTypesList)
        {
            if (goodsIdType.Contains(goodsId))
            {
                goodsIdsTypesList.Remove(goodsIdType);
                return goodsIdsTypesList;
            }
        }
        throw new Exception("removing from cart error");
    }

    private async Task<List<string>> AddInCart(string goodsId, string goodsType, List<string> goodsIdsTypesList)
    {
        foreach (var idType in goodsIdsTypesList)
        {
            if (idType.Contains(goodsId))
                throw new Exception("this goods is already in cart");
        }
        goodsIdsTypesList.Add($"{goodsId}{GoodsIdTypeSeparator}{goodsType}");
        return goodsIdsTypesList;
    }
}
