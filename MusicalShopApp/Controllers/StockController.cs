using BizLogicBase.Validation;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.StockServices;
using ViewModelsLayer.Stock;
using Microsoft.AspNetCore.Authorization;
using ViewModelsLayer.Stock.Delivery;
using ServiceLayer.GoodsServices;
using ViewModelsLayer.Common;
using ServiceLayer.StockServices.Delivery;
using ViewModelsLayer.Goods;
using DataLayer.Common;
using ServiceLayer.SalesServices;
using MusicalShopApp.Controllers.BaseControllers;
using ServiceLayer;

namespace MusicalShopApp.Controllers;

[Authorize(Policy = nameof(CommonNames.StockManager))]
public class StockController : GoodsListBaseController
{
    [HttpGet]
    public async Task<IActionResult> AddGoodsToWarehouse([FromServices] ISpecificTypeService specificTypesService)
    {
        var specificTypes = await specificTypesService.GetAllSpecificTypes();
        // TODO it must not be so awkward
        GoodsKindSpecificDataDto defaultSpecificData = new(KindOfGoods.MusicalInstruments, default, default, default, default, default, default);
        var defaultDto = new AddGoodsToWarehouseDto(default!, default!, false, default, default, GoodsStatus.InStock, default, default, defaultSpecificData, default, false, default);
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

    [HttpGet]
    public async Task<IActionResult> DeliverySearch([FromQuery] DeliveryFilterOptions filter, [FromQuery] DeliveryOrderByOptions orderBy, [FromQuery] PagingModel pagingModel, [FromServices] IGetRelevantDeliveriesService service)
    {
        List<DeliveryUnitSearchModel> deliveryUnitModels = [];
        var deliveries = await service.GetRelevantDeliveries(filter, orderBy, pagingModel);
        if (service.HasErrors)
        {
            deliveryUnitModels = [];
            ViewBag.Errors = service.Errors.Select(e => e.ErrorMessage);
        }
        else
            foreach (var delivery in deliveries!)
                deliveryUnitModels.Add(new DeliveryUnitSearchModel(delivery.GoodsDeliveryId, delivery.LocalExpectedDeliveryDate, delivery.LocalActualDeliveryDate, delivery.ActualDeliveryDate is not null));
        return View(new DeliverySearchModel(filter, orderBy, deliveryUnitModels));
    }

    [HttpGet("/stock/delivery/{deliveryId:Guid}")]
    // TODO context should not be here. but bad architecture forces
    public async Task<IActionResult> DeliveryUnit([FromRoute] Guid deliveryId, [FromServices] IGetDeliveryService service, [FromServices] IGetGoodsUnitsOfDeliveryService goodsService)
    {
        try
        {
            var delivery = await service.GetDelivery(deliveryId);
            var goodsItems = await goodsService.GetGoodsUnitsOfDelivery(deliveryId);

            DeliveryUnitModel deliveryModel = new(delivery.GoodsDeliveryId, delivery.LocalExpectedDeliveryDate, delivery.LocalActualDeliveryDate, delivery.ActualDeliveryDate is not null, MapToGoodsList(goodsItems));
            return View(deliveryModel);
        }
        catch (InvalidOperationException e)
        {
            //Response.StatusCode = 404;
#warning this page is weird
            return NotFound();
        }

    }

    [HttpGet("/delivery/accept/{deliveryId:Guid}")]
    public async Task<IActionResult> AcceptDelivery([FromRoute] Guid deliveryId, [FromServices] IGetGoodsUnitsOfDeliveryService goodsService)
    {
        var goodsItems = await goodsService.GetGoodsUnitsOfDelivery(deliveryId);
        ViewBag.Errors = TempData["Errors"];

        return View(new AcceptDeliveryDto(deliveryId, MapToGoodsList(goodsItems)));
    }

    [HttpPost("/delivery/accept")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AcceptDelivery([FromForm] AcceptDeliveryModel model, [FromServices] IAcceptDeliveryService service)
    {
        await service.AcceptDelivery(model);
        if (service.HasErrors)
        {
            TempData["Errors"] = service.Errors.Select(e => e.ErrorMessage).ToArray();
            return RedirectToAction("AcceptDelivery", new { deliveryId = model.DeliveryId });
        }

        return RedirectToAction("DeliveryUnit", new { deliveryId = model.DeliveryId });
    }
}
