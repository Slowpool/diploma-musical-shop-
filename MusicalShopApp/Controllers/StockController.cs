using BizLogicBase.Validation;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.StockServices;
using ViewModelsLayer.Stock;
using Microsoft.AspNetCore.Authorization;

namespace MusicalShopApp.Controllers;

[Authorize(Policy = nameof(CommonNames.StockManager))]
public class StockController : Controller
{
    [HttpGet]
    public async Task<IActionResult> AddGoodsToWarehouse([FromServices] ISpecificTypeService specificTypesService)
    {
        var specificTypes = await specificTypesService.GetAllSpecificTypes();
        // TODO it must not be so awkward
        GoodsKindSpecificDataDto defaultSpecificData = new(KindOfGoods.MusicalInstruments, default, default, default, default, default, default);
        var defaultDto = new AddGoodsToWarehouseDto(default!, default!, false, default, default, GoodsStatus.InStock, default, default, defaultSpecificData, default, false);
        return View(new AddGoodsToWarehouseModel(defaultDto, specificTypes, []));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<JsonResult> AddGoodsToWarehouse(AddGoodsToWarehouseDto addGoodsToWarehouseDto, [FromServices] IAddNewGoodsService addNewGoodsService)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(modelEntry => modelEntry.Errors.Select(e => e.ErrorMessage));
            // Unprocessable entity
            Response.StatusCode = 400;
            return Json(errors);
        }
        Guid? deliveryId = await addNewGoodsService.AddNewGoods(addGoodsToWarehouseDto);
        if (addNewGoodsService.HasErrors)
        {
            Response.StatusCode = 409;
            return Json(addNewGoodsService.Errors);
        }
        else
            return Json(new { success = true, deliveryId });
    }
}
