using ConstNames;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MusicalShopApp.Controllers
{
	[Authorize(Roles = ConstNames.CommonNames.AdminRole)]
	public class UsersController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
