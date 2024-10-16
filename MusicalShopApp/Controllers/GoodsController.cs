using System.Diagnostics;
using BusinessLogicLayer.Goods.Dto;
using DataLayer.Models.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicalShopApp.Models;
using MusicalShopApp.Models.Goods;
using ServiceLayer.GoodsServices;

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
    public async Task<IActionResult> Search([FromServices] GoodsService service, [FromQuery] string q, [FromQuery] string filter=nameof(GoodsFilter.None), [FromQuery] string orderBy=nameof(GoodsOrderBy.Relevance), [FromQuery] int page=1, [FromQuery] int pageSize=15)
    {
        var filterEnum = Enum.Parse<GoodsFilter>(value: filter, ignoreCase: true);
        var orderByEnum = Enum.Parse<GoodsOrderBy>(value: orderBy, ignoreCase: true);
#warning what about query object pattern here?
        var goodsIdsAndTypes = await service.GetRelevantGoodsIds(q, filterEnum, orderByEnum, page, pageSize);
        List<GoodsUnitSearchDto> goodsUnitModels = new();
        foreach (var (id, type) in goodsIdsAndTypes)
        {
            var goodsUnitSearchDto = await service.GetReadableGoodsInfo(id, type);
            if (goodsUnitSearchDto != null)
                goodsUnitModels.Add(goodsUnitSearchDto);
        }

        var goodsSearchModel = new GoodsSearchModel
        {
            ResearchText = q,
            Filter = filterEnum,
            OrderBy = orderByEnum,
            GoodsUnitModels = goodsUnitModels
        };
        return View(goodsSearchModel);
    }
}
