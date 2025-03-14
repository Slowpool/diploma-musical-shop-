using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicalShopApp.Controllers;

[Authorize(Policy = nameof(CommonNames.Admin))]
public class ReportsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
