using BusinessLogicLayer.AdminPanel.Dto;
using Common;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.AdminServices;

namespace MusicalShopApp.Controllers
{
    [Authorize(Roles = CommonNames.AdminRole)]
	public class AdminPanelController : Controller
	{
		//private readonly IServiceProvider services;
  //      public AdminPanelController(IServiceProvider services)
		//{
		//	this.services = services;
		//}

        public IActionResult Index()
		{
			return View();
		}

		[HttpPost(template: "/backup")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Backup([FromQuery] string? backupPath)
		{
#warning implement
			return RedirectToAction("Index");
		}

		[HttpGet(template: "/AdminPanel/Users/{userId:Guid}", Name = "SpecificUser")]
		public async Task<IActionResult> Users(Guid userId, [FromServices] IGetUserService service, [FromQuery] string? errors)
		{
			var user = await service.GetUserInfo(userId);
			ViewBag.Errors = errors;
			return View(user);
		}

		[HttpPost()]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UpdateUserDto dto, [FromServices] IUpdateUserService service)
		{
			await service.UpdateUser(dto);
			return RedirectToAction("Users", new RouteValueDictionary(
#warning workaround
				new { userId = dto.Id, errors = string.Join("SEP", service.Errors)}));
		}
    }
}
