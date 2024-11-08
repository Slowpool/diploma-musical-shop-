using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.StockServices;
using ViewModelsLayer.Stock;

namespace MusicalShopApp.Controllers;

public class StockController : Controller
{
    [HttpGet]
    public async Task<IActionResult> AddGoodsToWarehouse([FromServices] ISpecificTypesService specificTypesService)
    {
        var specificTypes = await specificTypesService.GetSpecificTypes();
        return View(new AddGoodsToWarehouseModel(null, specificTypes, []));
    }

    [HttpPost]
    public async Task<IActionResult> AddGoodsToWarehouse([FromServices] ISpecificTypesService specificTypesService, [FromServices] IAddNewGoodsService addNewGoodsService, AddGoodsToWarehouseDto addGoodsToWarehouseDto)
#warning try making it working
    {
#error how to add one of several kind of goods with specific attributes?
        var specificTypes = await specificTypesService.GetSpecificTypes();
        try
        {
            var specificTypeEntity = await specificTypesService.FindSpecificType(addGoodsToWarehouseDto.SpecificType);
            await addNewGoodsService.AddNewGoods(addGoodsToWarehouseDto.GoodsName, addGoodsToWarehouseDto.KindOfGoods, specificTypeEntity, addGoodsToWarehouseDto.Price, addGoodsToWarehouseDto.Status, addGoodsToWarehouseDto.Description, addGoodsToWarehouseDto.NumberOfUnits);

#warning display success/fail
            return View(new AddGoodsToWarehouseModel(addGoodsToWarehouseDto, specificTypes, []));
        }
        catch
        {
            return View(new AddGoodsToWarehouseModel(addGoodsToWarehouseDto, specificTypes, [.. addNewGoodsService.Errors]));
        }
    }
}
