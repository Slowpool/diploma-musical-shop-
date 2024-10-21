using System.Diagnostics;
using BusinessLogicLayer.Goods.Dto;
using DataLayer.Models;
using DataLayer.Models.SupportClasses;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicalShopApp.Models;
using MusicalShopApp.Models.Goods;
using ServiceLayer.GoodsServices;
using ServiceLayer.GoodsServices.Support;

namespace MusicalShopApp.Controllers;

[Authorize]
public class GoodsController : Controller
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

    [HttpGet(template: "/search")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Search([FromServices] GoodsService service, [FromQuery] string q, [FromQuery] int? minPrice, [FromQuery] int? maxPrice, [FromQuery] string? fromDate, [FromQuery] string? toDate, [FromQuery] string kindOfGoods=nameof(KindOfGoods.MusicalInstruments), [FromQuery] string orderBy=nameof(GoodsOrderBy.Relevance), [FromQuery] bool ascendingOrder=true, [FromQuery] int page=1, [FromQuery] int pageSize=15, [FromQuery] string status=nameof(GoodsStatus.InStock))
    {
        var kindOfGoodsEnum = Enum.Parse<KindOfGoods>(kindOfGoods, ignoreCase: true);
        var statusEnum = Enum.Parse<GoodsStatus>(kindOfGoods, ignoreCase: true);
#warning what about null here
        var fromDateTime = DateTime.Parse(fromDate);
        var toDateTime = DateTime.Parse(toDate);
        var filterOptions = new GoodsFilterOptions(minPrice, maxPrice, fromDateTime, toDateTime, kindOfGoodsEnum, statusEnum);
        var orderByEnum = Enum.Parse<GoodsOrderBy>(orderBy, ignoreCase: true);
        var orderByOptions = new GoodsOrderByOptions(orderByEnum, ascendingOrder);
#warning what about query object pattern here?
        var goodsIds = await service.GetRelevantGoodsIds(q, filterOptions, orderByOptions, page, pageSize);
#warning i don't like it
        var type = kindOfGoodsEnum == KindOfGoods.Accessories ? typeof(Accessory) :
            kindOfGoodsEnum == KindOfGoods.AudioEquipmentUnits ? typeof(AudioEquipmentUnit) :
            kindOfGoodsEnum == KindOfGoods.MusicalInstruments ? typeof(MusicalInstrument) :
            typeof(SheetMusicEdition);
        List<GoodsUnitSearchDto> goodsUnitModels = new();
        foreach (var id in goodsIds)
        {
            var goodsUnitSearchDto = await service.GetReadableGoodsInfo(id, type);
            if (goodsUnitSearchDto != null)
                goodsUnitModels.Add(goodsUnitSearchDto);
        }

        var goodsSearchModel = new GoodsSearchModel
        {
            ResearchText = q,
            Filter = filterOptions,
            OrderBy = orderByOptions,
            GoodsUnitModels = goodsUnitModels
        };
        return View(goodsSearchModel);
    }
}
