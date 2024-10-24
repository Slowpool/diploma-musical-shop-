using System;
using System.Diagnostics;
using System.Text;
using BusinessLogicLayer.Goods.Dto;
using Common;
using DataLayer.Common;
using DataLayer.Models;
using DataLayer.Models.SupportClasses;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicalShopApp.Models;
using MusicalShopApp.Models.Goods;
using NuGet.Packaging.Signing;
using ServiceLayer.GoodsServices;
using ServiceLayer.GoodsServices.Support;

namespace MusicalShopApp.Controllers;

[Authorize]
public class GoodsController : Controller
{
    private readonly ILogger<GoodsController> _logger;

    private string? GoodsIdsInCart => HttpContext.Session.GetString(CommonNames.SeparatedGoodsIdsInCart);

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

    [HttpGet]
    public async Task<IActionResult> Search([FromServices] GoodsService service, [FromQuery] string q, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] string? fromReceiptDate, [FromQuery] string? toReceiptDate, [FromQuery] string kindOfGoods = nameof(KindOfGoods.MusicalInstruments), [FromQuery] string orderBy = nameof(GoodsOrderBy.Relevance), [FromQuery] bool ascendingOrder = true, [FromQuery] int page = 1, [FromQuery] int pageSize = 15, [FromQuery] string status = nameof(GoodsStatus.InStock))
    {
        var kindOfGoodsEnum = Enum.Parse<KindOfGoods>(kindOfGoods, ignoreCase: true);
        var statusEnum = Enum.Parse<GoodsStatus>(status, ignoreCase: true);
        DateTime? fromReceiptDateTime = null;
        DateTime? toReceiptDateTime = null;
        if (DateTime.TryParse(fromReceiptDate, out DateTime temp))
            fromReceiptDateTime = temp;
        if (DateTime.TryParse(toReceiptDate, out temp))
            toReceiptDateTime = temp;
        var filterOptions = new GoodsFilterOptions(minPrice, maxPrice, fromReceiptDateTime, toReceiptDateTime, kindOfGoodsEnum, statusEnum);
        var orderByEnum = Enum.Parse<GoodsOrderBy>(orderBy, ignoreCase: true);
        var orderByOptions = new GoodsOrderByOptions(orderByEnum, ascendingOrder);
#warning what about query object pattern here?
        var goodsIds = await service.GetRelevantGoodsIds(q, filterOptions, orderByOptions, page, pageSize);
#warning it could be simpler
        var type = kindOfGoodsEnum == KindOfGoods.Accessories ? typeof(Accessory) :
            kindOfGoodsEnum == KindOfGoods.AudioEquipmentUnits ? typeof(AudioEquipmentUnit) :
            kindOfGoodsEnum == KindOfGoods.MusicalInstruments ? typeof(MusicalInstrument) :
            typeof(SheetMusicEdition);
        List<GoodsUnitSearchDto> goodsUnitModels = new();
        foreach (var id in goodsIds)
        {
            var goodsUnitSearchDto = await service.GetReadableGoodsInfo(id, type);
            if (goodsUnitSearchDto != null)
            {
                var goodsIdsInCart = HttpContext.Session.GetString(CommonNames.SeparatedGoodsIdsInCart);
                goodsUnitSearchDto.IsInCart = goodsIdsInCart != null && goodsIdsInCart.Split(CommonNames.GoodsIdSeparator).Contains(id);
                goodsUnitModels.Add(goodsUnitSearchDto);
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
        return View(goodsSearchModel);
    }

    [ValidateAntiForgeryToken]
    public async Task<ContentResult> AddToOrRemoveFromCart(string goodsId, bool isInCart, [FromServices] GoodsService service)
    {
        string? goodsIds = GoodsIdsInCart;
        string? updatedgoodsIds = await service.AddToOrRemoveFromCart(goodsId, isInCart, goodsIds);
        if (updatedgoodsIds == null)
            return Content("goods id or request were strange");
        HttpContext.Session.SetString(CommonNames.SeparatedGoodsIdsInCart, updatedgoodsIds);
        return Content("success");
    }

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> AddToOrRemoveFromCart(Guid goodsId, bool addToCart)
    //{
    //    return RedirectToAction("Search");
    //}

    public async Task<IActionResult> Cart([FromServices] GoodsService service)
    {
#warning probably the same code as in search
        List<GoodsUnitSearchDto> GoodsUnitModels = new();
        var goodsIds = GoodsIdsInCart;
        if (!string.IsNullOrEmpty(goodsIds))
        {
            foreach(var goodsId in goodsIds.Split(','))
            {
                var goodsInfo = await service.GetReadableGoodsInfo(goodsId, await service.GetGoodsType(goodsId));
                if (goodsInfo != null)
                    GoodsUnitModels.Add(goodsInfo);
            }
        }
        return View(GoodsUnitModels);
    }
}
