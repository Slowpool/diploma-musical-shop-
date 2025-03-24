using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModelsLayer.Reports;

namespace MusicalShopApp.Controllers;

[Authorize(Policy = nameof(CommonNames.Admin))]
public class ReportsController : Controller
{
    public IActionResult Index()
    {
        var model = new ReportGeneralOptions(default, default, ReportType.General, null, );
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Generate([FromForm] ReportGeneralOptions options)
    {
        return View(options);
    }
}
