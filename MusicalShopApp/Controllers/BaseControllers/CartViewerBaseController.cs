using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Mvc;

namespace MusicalShopApp.Controllers.BaseControllers;

public abstract class CartViewerBaseController : Controller
{
    public string? GoodsIdsAndKindsInCart => HttpContext.Session.GetString(CommonNames.GoodsIdsInCartKey);
    public string[] GoodsIdsAndKinds => GoodsIdsAndKindsInCart?.Split(CommonNames.GoodsIdSeparator, StringSplitOptions.RemoveEmptyEntries) ?? [];
    public bool IsInCart(Guid goodsId) => GoodsIdsAndKindsInCart != null && GoodsIdsAndKinds!.Any(s => s.Contains(goodsId.ToString()));
    public void SetNewCartValue(string newValue) => HttpContext.Session.SetString(CommonNames.GoodsIdsInCartKey, newValue);
}
