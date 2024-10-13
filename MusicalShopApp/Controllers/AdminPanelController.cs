using ConstNames;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.AdminServices;
using ServiceLayer.AdminServices.Dto;

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

		[HttpGet(template: "/AdminPanel/Users/{userId:Guid}")]
		public async Task<IActionResult> Users(Guid userId, [FromServices] IUserService service)
		{
			var user = await service.GetUserInfo(userId);
			//// user-unfriendly way
			//if (user == null)
			//{
			//	return BadRequest("User with such an id was not found");
			//}
			return View(user);
		}

		[HttpPost()]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(UpdateUserDto dto, [FromServices] IUserService service)
		{
			if (!await service.UpdateUser(dto))
			{
				return BadRequest("something went wrong");
			}
			return RedirectToAction("Users", new { userId = dto.Id });
		}
    }
}
