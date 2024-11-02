using Common;
using Microsoft.AspNetCore.Mvc;

namespace MusicalShopApp.Controllers.BaseControllers;

public abstract class CartViewerBaseController : Controller
{
    public string? GoodsIdsInCart => HttpContext.Session.GetString(CommonNames.GoodsIdsInCartKey);
    public string[]? GoodsIdsAndTypes => GoodsIdsInCart?.Split(CommonNames.GoodsIdSeparator, StringSplitOptions.RemoveEmptyEntries);
    public static string GetGoodsId(string goodsIdAndType) => goodsIdAndType.Split(CommonNames.GoodsIdTypeSeparator)[0];
    public bool IsInCart(string goodsId) => GoodsIdsInCart != null && GoodsIdsAndTypes!.Any(s => s.Contains(goodsId));

    public static Type GetGoodsType(string goodsIdAndType) => Type.GetType($"DataLayer.Models.{goodsIdAndType.Split(CommonNames.GoodsIdTypeSeparator)[1]}, DataLayer")!;

    public void SetNewCartValue(string newValue) => HttpContext.Session.SetString(CommonNames.GoodsIdsInCartKey, newValue);
}
