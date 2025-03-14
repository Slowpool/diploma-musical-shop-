using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicalShopApp.Controllers;

[Authorize(Policy = nameof(CommonNames.Seller))]
public class PurchaseReturnController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
