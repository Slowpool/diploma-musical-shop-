using BizLogicBase.Validation;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.StockServices;
using ViewModelsLayer.Stock;

namespace MusicalShopApp.Controllers;

public class StockController : Controller
{
    [HttpGet]
    public async Task<IActionResult> AddGoodsToWarehouse([FromServices] ISpecificTypeService specificTypesService)
    {
        var specificTypes = await specificTypesService.GetAllSpecificTypes();
        var defaultDto = new AddGoodsToWarehouseDto(default, KindOfGoods.MusicalInstruments, default, false, default, default, GoodsStatus.InStock, default, default, default);
        return View(new AddGoodsToWarehouseModel(defaultDto, specificTypes, []));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ContentResult> AddGoodsToWarehouse([FromServices] IAddNewGoodsService addNewGoodsService, AddGoodsToWarehouseDto addGoodsToWarehouseDto)
    {
        if (!ModelState.IsValid)
            // TODO what data? how to get errors of model?
            return Content("Некорректные данные. ");
        await addNewGoodsService.AddNewGoods(addGoodsToWarehouseDto);
        string result = addNewGoodsService.HasErrors
            ? string.Join("\r\n", addNewGoodsService.Errors)
            : "Товар успешно добавлен";
        return Content(result);
    }
}
