using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MusicalShopApp.Controllers.BaseControllers;
using MusicalShopApp.Models;
using ServiceLayer.GoodsServices;
using ServiceLayer.GoodsServices.Extensions;
using ServiceLayer.SalesServices;
using ViewModelsLayer;
using ViewModelsLayer.Goods;

namespace MusicalShopApp.Controllers;

[Authorize(Roles = $"{CommonNames.AdminRole},{CommonNames.StockManagerRole},{CommonNames.SellerRole},{CommonNames.ConsultantRole}")]
public class GoodsController : CartViewerBaseController
{
    private readonly ILogger<GoodsController> _logger;

    public GoodsController(ILogger<GoodsController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Search(GoodsSearchModel model)
    //{
    //    //GoodsSearchModel model = new()
    //    //{
    //    //    Filter = new GoodsFilterOptions(null, null, null, null, KindOfGoods: KindOfGoods.MusicalInstruments,
    //    //        Status: GoodsStatus.InStock),
    //    //    GoodsUnitModels = [],
    //    //    OrderBy = new GoodsOrderByOptions(GoodsOrderBy.Relevance, true),
    //    //    ResearchText = string.Empty
    //    //};
    //    return View(model);
    //}
#warning what the hell is going on here? i have not been seeing this method for just about 1 week and now it looks like insane one
    [HttpGet]
    public async Task<IActionResult> Search([FromServices] IGetRelevantGoodsService getRelevantGoodsService, [FromServices] IGetGoodsService getGoodsService, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] DateTime? fromReceiptDate, [FromQuery] DateTime? toReceiptDate, [FromQuery] KindOfGoods kindOfGoods, [FromQuery] GoodsOrderBy orderBy, [FromQuery] GoodsStatus status, [FromQuery] bool ascendingOrder, [FromQuery] string q = "", [FromQuery] int page = 1, [FromQuery] int pageSize = 15)
    {
        var filterOptions = new GoodsFilterOptions(minPrice, maxPrice, fromReceiptDate.LocalToUniversal(), toReceiptDate.LocalToUniversal(), kindOfGoods, status);
        var orderByOptions = new GoodsOrderByOptions(orderBy, ascendingOrder);
#warning what about query object pattern here?
        var goodsIds = await getRelevantGoodsService.GetRelevantGoodsIds(q, filterOptions, orderByOptions, page, pageSize);
        List<GoodsUnitSearchDto> goodsUnitModels = new();
        foreach (var goodsId in goodsIds)
        {
            try
            {
                var goodsUnitSearchDto = await getGoodsService.GetReadableGoodsInfo(goodsId, kindOfGoods);
                goodsUnitSearchDto.IsInCart = IsInCart(goodsId);
                goodsUnitModels.Add(goodsUnitSearchDto);
            }
            catch
            {
                _logger.LogWarning("unknown goods id in cart: {goodsId}", goodsId);
            }
        }

        var goodsSearchModel = new GoodsSearchModel
        {
            ResearchText = q,
            Filter = filterOptions,
            OrderBy = orderByOptions,
            GoodsUnitModels = goodsUnitModels,
            ResultsCount = goodsUnitModels.Count()
        };
        ViewBag.Session = GoodsIdsAndKindsInCart;
        return View(goodsSearchModel);
    }

    [ValidateAntiForgeryToken]
    [Authorize(Roles = CommonNames.SellerRole)]
    public async Task<ContentResult> AddToOrRemoveFromCart([FromServices] ICartService cartService, Guid goodsId, bool isInCart)
    {
        string? newGoodsIdsAndTypes = await cartService.AddToOrRemoveFromCart(goodsId, isInCart, GoodsIdsAndKindsInCart);
        if (newGoodsIdsAndTypes == null)
            return Content("failed");
        SetNewCartValue(newGoodsIdsAndTypes);
        ViewBag.Session = GoodsIdsAndKindsInCart;
        return Content("success");
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> AddToOrRemoveFromCart(Guid goodsId, bool addToCart)
    //{
    //    return RedirectToAction("Search");
    //}

    [Authorize(Roles = CommonNames.SellerRole)]
    public async Task<IActionResult> Cart([FromServices] IGetGoodsService service)
    {
#warning probably the same code as in search
        List<GoodsUnitSearchDto> GoodsUnitModels = new();
        if (!string.IsNullOrEmpty(GoodsIdsAndKindsInCart))
        {
            foreach (var goodsIdAndType in GoodsIdsAndKinds!)
            {
                Guid goodsId = Guid.Parse(CutGoodsId(goodsIdAndType));
                var goodsInfo = await service.GetReadableGoodsInfo(goodsId, CutGoodsKind(goodsIdAndType));
                goodsInfo.IsInCart = IsInCart(goodsId);
                GoodsUnitModels.Add(goodsInfo);
                //try
                //{
                //    var goodsInfo = await service.GetReadableGoodsInfo(goodsId, CutGoodsKind(goodsIdAndType));
                //    goodsInfo.IsInCart = IsInCart(goodsId);
                //    GoodsUnitModels.Add(goodsInfo);
                //}
                //catch
                //{
                //    _logger.LogError("unknown goods id in cart: {goodsId}", goodsId);
                //}
            }
        }
        ViewBag.Session = GoodsIdsAndKindsInCart;
        return View(GoodsUnitModels);
    }
}
