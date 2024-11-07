using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> AddGoodsToWarehouse([FromServices] ISpecificTypesService specificTypesService, [FromServices] IAddNewGoodsService addNewGoodsService, string goodsName, KindOfGoods kindOfGoods, string specificType, int price, GoodsStatus status, string description, int numberOfUnits)
#warning try making it working
    //public async Task<IActionResult> AddGoodsToWarehouse([FromServices] IFindSpecificTypeService findSpecificTypeService, [FromServices] IAddNewGoodsService addNewGoodsService, addGoodsToWarehouseModel)
    {
        var specificTypes = await specificTypesService.GetSpecificTypes();
        try
        {
            var specificTypeEntity = await specificTypesService.FindSpecificType(specificType);
            await addNewGoodsService.AddNewGoods(goodsName, kindOfGoods, specificTypeEntity, price, status, description, numberOfUnits);
#warning display success/fail
            return View(new AddGoodsToWarehouseModel(new AddGoodsToWarehouseDto(goodsName, kindOfGoods, specificType, price, status, description, numberOfUnits), specificTypes, []));
        }
        catch
        {
            return View(new AddGoodsToWarehouseModel(new AddGoodsToWarehouseDto(goodsName, kindOfGoods, specificType, price, status, description, numberOfUnits), specificTypes, [.. addNewGoodsService.Errors]));
        }
    }
}
