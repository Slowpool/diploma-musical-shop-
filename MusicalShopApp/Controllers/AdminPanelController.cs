using ConstNames;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

		[HttpPost(template: "/backup")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Backup([FromQuery] string? backupPath)
		{
			return RedirectToAction("Index");
		}

		[HttpGet(template: "/AdminPanel/Users/{userId:Guid}")]
		public async Task<IActionResult> Users(Guid userId)
		{
#warning rewrite
			var userModel = new AppUser() { Id = userId.ToString() };
			return View(userModel);
		}
    }
}
