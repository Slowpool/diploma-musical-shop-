using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.AdminServices;
using ServiceLayer.GoodsServices;
using ViewModelsLayer.Reports;

namespace MusicalShopApp.Controllers;

[Authorize(Policy = nameof(CommonNames.Admin))]
public class ReportsController : Controller
{
    [HttpGet("/reports")]
    public async Task<IActionResult> Index([FromServices] ISpecificTypesParserService specificTypesService)
    {
        return View(new ReportCommonOptionsDto(null, null, null, null, null, [.. Enum.GetValues<KindOfGoods>()], null, await specificTypesService.Parse()));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Generate([FromForm] ReportCommonOptions options, [FromServices] IReportGeneratorService generatorService, [FromServices] ISpecificTypesParserService specificTypesService)
    {
        var reportItems = await generatorService.Generate(options);
        var optionsDto = new ReportCommonOptionsDto(options.FromDate, options.ToDate, options.Subtype, options.Type, options.ChartType, options.KindsOfGoodsForGeneral, options.KindOfGoodsForSpecific, await specificTypesService.Parse());

        return View(new GeneratedReportModel(reportItems, optionsDto));
    }
}
