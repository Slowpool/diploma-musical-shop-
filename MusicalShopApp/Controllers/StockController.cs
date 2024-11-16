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
        var defaultDto = new AddGoodsToWarehouseDto(default, KindOfGoods.MusicalInstruments, default, default, GoodsStatus.InStock, default, default, default);
        return View(new AddGoodsToWarehouseModel(defaultDto, specificTypes, []));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddGoodsToWarehouse([FromServices] ISpecificTypeService specificTypesService, [FromServices] IAddNewGoodsService addNewGoodsService, AddGoodsToWarehouseDto addGoodsToWarehouseDto)
    {
        var specificTypes = await specificTypesService.GetAllSpecificTypes();
        try
        {
            await addNewGoodsService.AddNewGoods(addGoodsToWarehouseDto);
#warning display success/fail
        }
        catch
        { }
        return View(new AddGoodsToWarehouseModel(addGoodsToWarehouseDto, specificTypes, [.. addNewGoodsService.Errors]));
    }
}
