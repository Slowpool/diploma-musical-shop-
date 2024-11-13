using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicalShopApp.Controllers
{
	[Authorize(Roles = CommonNames.SellerRole)]
	public class PurchaseReturnController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
