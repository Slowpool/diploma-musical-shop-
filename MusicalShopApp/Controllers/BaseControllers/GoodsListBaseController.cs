using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Mvc;
using ViewModelsLayer.Goods;

namespace MusicalShopApp.Controllers.BaseControllers;

public class GoodsListBaseController : CartViewerBaseController
{
    public List<GoodsUnitSearchModel> MapToGoodsList(Dictionary<KindOfGoods, List<Goods>> goodsItems)
    {
        List<GoodsUnitSearchModel> goodsItemModels = [];
        foreach (var kindOfGoods in goodsItems.Keys)
        {
            var goodsItemsSpecific = goodsItems[kindOfGoods];
            foreach (var goodsItem in goodsItemsSpecific)
                goodsItemModels.Add(new()
                {
                    GoodsId = goodsItem.GoodsId,
                    // TODO
                    //Type =,
                    Name = goodsItem.Name,
                    Description = goodsItem.Description,
                    IsInCart = IsInCart(goodsItem.GoodsId),
                    KindOfGoods = kindOfGoods,
                    Price = goodsItem.Price,
                    Status = goodsItem.Status
                });
        }
        return goodsItemModels;
    }
}
