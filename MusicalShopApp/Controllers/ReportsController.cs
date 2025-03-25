using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.GoodsServices;
using ViewModelsLayer.Reports;

namespace MusicalShopApp.Controllers;

[Authorize(Policy = nameof(CommonNames.Admin))]
public class ReportsController : Controller
{
    [HttpGet("/reports")]
    public async Task<IActionResult> Index([FromServices] IMapKindOfGoodsService kindOfgoodsService)
    {
        Dictionary<KindOfGoods, Dictionary<Guid, string>> dict = [];
        foreach(var kindOfGoods in Enum.GetValues<KindOfGoods>())
        {
            dict[kindOfGoods] = await kindOfgoodsService.MapToSpecificTypes(kindOfGoods)
                .Select(st => new { st.SpecificTypeId, st.Name })
                .ToDictionaryAsync(st => st.SpecificTypeId, st => st.Name);
        }
        return View(new ReportGeneralOptionsModel(DateTime.Now, DateTime.Now, null, [.. Enum.GetValues<KindOfGoods>()], null, dict));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Generate([FromForm] ReportGeneralOptionsDto options)
    {
        return View(options);
    }
}
