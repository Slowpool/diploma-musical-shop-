using DataLayer.Common;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using ServiceLayer.GoodsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Common.CommonNames;

namespace ServiceLayer.SalesServices;

public interface ICartService
{

    Task<string> MoveGoodsBackToCart(Guid saleId);
    Task<string?> AddToOrRemoveFromCart(Guid goodsId, bool isInCart, string? goodsIdsAndKinds);
    Task<List<Goods>> GetGoodsFromCart(string[] cartContent);
    // Raw logic methods
    string CutGoodsId(string goodsIdAndKind);
    KindOfGoods CutGoodsKind(string goodsIdAndKind);
}
#warning so where is services for adding to and removing from the cart
#warning UPD: what did i mean?
#warning UPD2: i meant that the cart is supposed to have methods like AddGoodsUnitInCart() and RemoveGoodsUnitFromCart()
public class CartService(MusicalShopDbContext context, IGetGoodsUnitsRelatedToSaleService goodsRelatedToSaleService, IGetGoodsService getGoodsService, IUpdateGoodsStatusService updateGoodsStatusService) : ICartService
{
    public string CutGoodsId(string goodsIdAndKind) => goodsIdAndKind.Split(CommonNames.GoodsIdAndKindSeparator)[0];
    public KindOfGoods CutGoodsKind(string goodsIdAndKind) => Enum.Parse<KindOfGoods>(goodsIdAndKind.Split(CommonNames.GoodsIdAndKindSeparator)[1])!;

    public async Task<string> MoveGoodsBackToCart(Guid saleId)
    {
        var goods = await goodsRelatedToSaleService.GetOrigGoodsUnitsRelatedToSale(saleId);
        foreach (var goodsUnit in goods)
        {
            goodsUnit.Status = GoodsStatus.InStock;
            context.Update(goodsUnit);
        }
        context.SaveChanges();

        // TODO change to kind of goods
        return string.Join(GoodsIdSeparator, goods.Select(goodsUnit => new { goodsUnit.GoodsId, TypeName = goodsUnit.GetType().Name }));

    }

    // dirty stuff
    public async Task<string> AddToOrRemoveFromCart(Guid goodsId, bool isInCart, string? goodsIdsAndKinds)
    {
        List<string> goodsIdsAndKindsList = goodsIdsAndKinds?.Split(GoodsIdSeparator, StringSplitOptions.RemoveEmptyEntries)
                                                            ?.ToList() ?? [];
        var kindOfGoods = await getGoodsService.GetGoodsKind(goodsId);
#warning is it validation? did i try to validate whether goods unit exists in db?
        var goods = await getGoodsService.GetGoodsInfo(goodsId, kindOfGoods);
        if (isInCart)
            RemoveFromCart(goodsId, goodsIdsAndKindsList);
        else
            AddInCart(goodsId, kindOfGoods, goodsIdsAndKindsList);
        await updateGoodsStatusService.UpdateGoodsStatus(goods.GoodsId, kindOfGoods, isInCart ? GoodsStatus.InStock : GoodsStatus.InCart);
        return string.Join(GoodsIdSeparator, goodsIdsAndKindsList);
    }

    private List<string> RemoveFromCart(Guid goodsId, List<string> goodsIdsAndKindsList)
    {
        foreach (var goodsIdAndKind in goodsIdsAndKindsList)
        {
            if (goodsIdAndKind.Contains(goodsId.ToString()))
            {
                goodsIdsAndKindsList.Remove(goodsIdAndKind);
                return goodsIdsAndKindsList;
            }
        }
        throw new Exception("removing from cart error");
    }

    private List<string> AddInCart(Guid goodsId, KindOfGoods kindOfGoods, List<string> goodsIdsAndKindsList)
    {
        foreach (var goodsIdAndKind in goodsIdsAndKindsList)
        {
            if (goodsIdAndKind.Contains(goodsId.ToString()))
                throw new Exception("this goods is already in cart");
        }
        goodsIdsAndKindsList.Add($"{goodsId}{GoodsIdAndKindSeparator}{kindOfGoods}");
        return goodsIdsAndKindsList;
    }

    public async Task<List<Goods>> GetGoodsFromCart(string[] cartContent)
    {
        List<Goods> goodsList = [];
        foreach (var goodsIdAndType in cartContent)
        {
            goodsList.Add(await getGoodsService.GetGoodsInfo(Guid.Parse(CutGoodsId(goodsIdAndType)), CutGoodsKind(goodsIdAndType)));
        }
        return goodsList;
    }
}
