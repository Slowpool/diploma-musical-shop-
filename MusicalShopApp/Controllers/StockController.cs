using BizLogicBase.Validation;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        GoodsKindSpecificDataDto defaultSpecificData = new(KindOfGoods.MusicalInstruments, default, default, default, default, default, default);
        var defaultDto = new AddGoodsToWarehouseDto(default!, default!, false, default, default, GoodsStatus.InStock, default, default, defaultSpecificData);
        return View(new AddGoodsToWarehouseModel(defaultDto, specificTypes, []));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ContentResult> AddGoodsToWarehouse([FromServices] IAddNewGoodsService addNewGoodsService, AddGoodsToWarehouseDto addGoodsToWarehouseDto)
    {
        if (!ModelState.IsValid)
        {
            string errors = string.Empty;
            foreach (var error in ModelState.Values.SelectMany(modelEntry => modelEntry.Errors.Select(e => e.ErrorMessage)))
            {
                errors += error + "<br>";
            }
            return Content(errors);
        }
        // TODO what data? how to get errors of model?
        await addNewGoodsService.AddNewGoods(addGoodsToWarehouseDto);
        string result = addNewGoodsService.HasErrors
            ? string.Join("\r\n", addNewGoodsService.Errors)
            : "Товар успешно добавлен";
        return Content(result);
    }
}
