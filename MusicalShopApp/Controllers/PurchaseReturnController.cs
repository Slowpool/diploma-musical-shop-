using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicalShopApp.Controllers
{
	[Authorize]
	public class PurchaseReturnController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
