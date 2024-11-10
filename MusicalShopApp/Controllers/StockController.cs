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
        var specificTypes = await specificTypesService.GetSpecificTypes();
        return View(new AddGoodsToWarehouseModel(null, specificTypes, []));
    }

    [HttpPost]
    public async Task<IActionResult> AddGoodsToWarehouse([FromServices] ISpecificTypeService specificTypesService, [FromServices] IAddNewGoodsService addNewGoodsService, AddGoodsToWarehouseDto addGoodsToWarehouseDto)
    {
        var specificTypes = await specificTypesService.GetSpecificTypes();
        try
        {
            await addNewGoodsService.AddNewGoods(addGoodsToWarehouseDto);
#warning display success/fail
            return View(new AddGoodsToWarehouseModel(addGoodsToWarehouseDto, specificTypes, []));
        }
        catch
        {
            return View(new AddGoodsToWarehouseModel(addGoodsToWarehouseDto, specificTypes, [.. addNewGoodsService.Errors]));
        }
    }
}
