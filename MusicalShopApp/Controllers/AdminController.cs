using BusinessLogicLayer.Admin.Dto;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ServiceLayer.AdminServices;
using ViewModelsLayer.Admin;

namespace MusicalShopApp.Controllers;

[Authorize(Policy = nameof(CommonNames.Admin))]
public class AdminController : Controller
{
    [HttpGet]
    public IActionResult Users()
    {
        return View();
    }

    [HttpGet(template: "/Admin/Users/{userId:Guid}")]
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
            // TODO handle in
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
    [ValidateAntiForgeryToken]
    public async Task<ContentResult> CreateBackup([FromForm] string note, [FromServices] IBackupService service)
    {
        if (ModelState.IsValid)
        {
            try
            {
                string fullFileName = await service.CreateBackup(note);
                return Content($"Успешно. Название файла: {fullFileName}");
            }
            catch
            {
#warning what error?
                return Content("Ошибка.");
            }
        }
        else
            return Content("Необходимо добавить примечание");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ContentResult> RestoreBackup([FromForm] DateTime? dateTime, [FromServices] IBackupService service)
    {
        if (dateTime is null)
            return Content("Укажите резервную копию для восстановления");
        await service.ApplyRestoreFromBackup((DateTime)dateTime);
        if (service.HasErrors)
#warning i don't think it's correctly. The view details in the controller?
            return Content(string.Join("<br>", service.Errors));
        return Content("База данных восстановлена");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
#warning rename to EditUsers
    public async Task<IActionResult> Edit(UpdateUserDto dto, [FromServices] IUpdateUserService service)
    {
        await service.UpdateUser(dto);
        return RedirectToAction("Users", new RouteValueDictionary(
#warning workaround
            new { userId = dto.Id, errors = string.Join("SEP", service.Errors) }));
    }

    [HttpPost]
    //[ValidateAntiForgeryToken]
    public async Task<JsonResult> UpdateUserRole([FromForm] UpdateUserRoleDto dto, [FromServices] IUpdateUserRoleService service)
    {
        await service.UpdateRole(dto);
        if (service.HasErrors)
        {
            Response.StatusCode = 409;
            return Json(service.Errors);
        }
        else
            return Json(new { success = true });
    }
}
