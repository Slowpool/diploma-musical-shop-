using BusinessLogicLayer.Admin.Dto;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.AdminServices;
using ViewModelsLayer.Admin;

namespace MusicalShopApp.Controllers
{
    [Authorize(Roles = CommonNames.AdminRole)]
	public class AdminController : Controller
	{
		[HttpGet]
        public IActionResult Users()
		{
			return View();
		}

        [HttpGet(template: "/Admin/Users/{userId:Guid}", Name = "SpecificUser")]
        public async Task<IActionResult> SpecificUser(Guid userId, [FromServices] IGetUserService service, [FromQuery] string? errors)
        {
            var user = await service.GetUserInfo(userId);
            ViewBag.Errors = errors;
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Backups([FromServices] IBackupService service)
        {
			try
			{
				var dict = await service.GetBackups();
				return View(new BackupsModel(dict));
			}
			catch
			{
#warning handle errors
				return View(null);
			}

        }

        [HttpPost]
		public async Task<ContentResult> CreateBackup([FromBody] string note, [FromServices] IBackupService service)
		{
			try
			{
				string fullFileName = await service.CreateBackup(note);
				return Content("Успешно. Название файла: ");
			}
			catch
			{
#warning what error?
				return Content("Ошибка.");
            }
        }

		[HttpPost]
        public async Task<ContentResult> RestoreBackup([FromBody] DateTime dateTime, [FromServices] IBackupService service)
		{
			return Content("Успех");
		}

        [HttpPost()]
		[ValidateAntiForgeryToken]
#warning rename to EditUsers
		public async Task<IActionResult> Edit(UpdateUserDto dto, [FromServices] IUpdateUserService service)
		{
			await service.UpdateUser(dto);
			return RedirectToAction("Users", new RouteValueDictionary(
#warning workaround
				new { userId = dto.Id, errors = string.Join("SEP", service.Errors)}));
		}
    }
}
