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
using ViewModelsLayer.Sales;

namespace MusicalShopApp.Controllers;

[Authorize(Policy = nameof(CommonNames.Consultant))]
public class GoodsController(ILogger<GoodsController> logger) : CartViewerBaseController
{
	private readonly ILogger<GoodsController> _logger = logger;

	[HttpGet]
	public IActionResult Index()
	{
#warning if i don't need it, delete view and this method and then change the url for ������ in _Layout
		return RedirectToAction("Search");
	}

	[HttpGet]
	public IActionResult Privacy()
	{
		return View();
	}

	[HttpGet]
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
		List<GoodsUnitSearchModel> goodsUnitModels = new();
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

	[HttpPost]
	[Authorize(Policy = nameof(CommonNames.Seller))]
	[ValidateAntiForgeryToken]
	public async Task<bool> AddToOrRemoveFromCart(Guid goodsId, bool isInCart, [FromServices] ICartService cartService)
	{
		// DRN violation
		string? newGoodsIdsAndTypes = await cartService.AddToOrRemoveFromCart(goodsId, isInCart, GoodsIdsAndKindsInCart);
		if (newGoodsIdsAndTypes == null)
			//return Content("failed");
			//throw new HttpRequestException("Failed");
			return false;
		SetNewCartValue(newGoodsIdsAndTypes);
		ViewBag.Session = GoodsIdsAndKindsInCart;
		return true; // ? Content() : Content("Added to cart");
	}

	[HttpGet]
	[Authorize(Policy = nameof(CommonNames.Seller))]
	public async Task<IActionResult> Cart([FromServices] IGetGoodsService getGoodsService, [FromServices] ICartService cartService)
	{
#warning i'm confused around the whole this cart stuff. i stopped understand what's going on here
		List<GoodsUnitSearchModel> GoodsUnitModels = new();
		if (!string.IsNullOrEmpty(GoodsIdsAndKindsInCart))
		{
			foreach (var goodsIdAndType in GoodsIdsAndKinds!)
			{
				Guid goodsId = Guid.Parse(cartService.CutGoodsId(goodsIdAndType));
				var goodsInfo = await getGoodsService.GetReadableGoodsInfo(goodsId, cartService.CutGoodsKind(goodsIdAndType));
				goodsInfo.IsInCart = IsInCart(goodsId);
				GoodsUnitModels.Add(goodsInfo);
				//try
				//{
				//    var goodsInfo = await service.GeitReadableGoodsInfo(goodsId, CutGoodsKind(goodsIdAndType));
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

	[HttpGet("/goods/{kindOfGoods}/{goodsId}")]
	public async Task<IActionResult> Unit([FromRoute] KindOfGoods kindOfGoods, [FromRoute] Guid goodsId, [FromServices] IGetGoodsService service, [FromServices] ISaleMapService saleMapService)
	{
		dynamic goods = await service.GetOrigGoodsItem(goodsId, kindOfGoods, true);
        List<SaleSearchModel> sales = [];
        foreach (var sale in goods.Sales)
        {
            sales.Add(await saleMapService.MapIdToSearchModel(sale.SaleId));
        }
		return View(new GoodsUnitModel(goods.GoodsId, kindOfGoods, goods.Name, goods.Price, goods.Status, goods.Description, goods.SpecificType.Name, goods.LocalReceiptDate, goods.DeliveryId, sales));
	}
}
