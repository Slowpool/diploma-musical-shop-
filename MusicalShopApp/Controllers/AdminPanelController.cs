using ConstNames;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicalShopApp.Controllers
{
	[Authorize(Roles = CommonNames.AdminRole)]
	public class AdminPanelController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
