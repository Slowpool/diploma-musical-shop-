﻿using DataLayer.Models;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicalShopApp.Controllers.BaseControllers;
using ServiceLayer;
using ServiceLayer.GoodsServices;
using ServiceLayer.SalesServices;
using ViewModelsLayer.Goods;
using ViewModelsLayer.Sales;

namespace MusicalShopApp.Controllers;

[Authorize(Policy = nameof(CommonNames.Seller))]
public class SalesController : GoodsListBaseController
{
    [HttpGet("/sales/search")]
    public async Task<IActionResult> Search(string q, [FromServices] IGetRelevantSalesService service, DateTime? minSaleDate, DateTime? maxSaleDate, DateTime? minReservationDate, DateTime? maxReservationDate, DateTime? minReturningDate, DateTime? maxReturningDate, SalePaidBy? paidBy, SaleStatus? status, SalesOrderBy orderBy = SalesOrderBy.Relevance, bool orderByAscending = true)
    {
        var filterOptions = new SalesFilterOptions(minSaleDate, maxSaleDate, minReservationDate, maxReservationDate, minReturningDate, maxReturningDate, status, paidBy);
        var orderByOptions = new SalesOrderByOptions(orderBy, orderByAscending);
        List<SaleSearchModel> list = await service.GetRelevantSales(q, filterOptions, orderByOptions);
        return View(new SalesSearchModel(q, list, list.Count, filterOptions, orderByOptions));
    }

    [HttpPost("/sale/arrange")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSaleAsNotPaid([FromServices] ICartService cartService, [FromServices] ICreateSaleService createSaleService)
    {
        var goods = await cartService.GetGoodsFromCart(GoodsIdsAndKinds);
        Guid? saleId = await createSaleService.CreateSaleAsNotPaid(goods);
        if (!createSaleService.HasErrors)
        {
            ClearSessionCart();
            return RedirectToAction("PayForSale", new { saleId });
        }
        else
            // TODO pass errors
            return RedirectToAction("Cart", "Goods");//, new SaleErrorModel(service.Errors));
    }

    [HttpPost("/reservation/create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSaleAsReserved(string secretWord, [FromServices] ICartService cartService, [FromServices] IReservationService createReservationService)
    {
        var goods = await cartService.GetGoodsFromCart(GoodsIdsAndKinds);
        Guid? reservationId = await createReservationService.CreateReservationAsNotComplete(goods, secretWord);
        if (!createReservationService.HasErrors)
        {
            ClearSessionCart();

            return RedirectToAction("Unit", "Sales", new { saleId = reservationId });
        }
        else
            // TODO flash stuff
            return RedirectToAction("Cart", "Goods");//, new SaleErrorModel(service.Errors));
    }

    [HttpGet("pay-for/{saleId:Guid}")]
    public async Task<IActionResult> PayForSale([FromRoute] Guid saleId)
    {
        return View(saleId);
    }

    /// <summary>
    /// Payment was successful.
    /// </summary>
    /// <param name="saleId"></param>
    /// <returns></returns>
    // TODO encapsulate the goods status updating
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterSaleAsSold(Guid saleId, SalePaidBy paidBy, [FromServices] IExistingSaleManagementService saleService, [FromServices] ICartService cartService, [FromServices] IGetGoodsUnitsOfSaleService goodsService, [FromServices] IUpdateGoodsStatusService goodsStatusService, [FromServices] IMapKindOfGoodsService kindOfGoodsService)
    {
        string result;
        try
        {
            await saleService.RegisterSaleAsPaid(saleId, paidBy);
            var goods = await goodsService.GetGoodsModelsOfSale(saleId);
            foreach (var goodsUnit in goods)
            {
                await goodsStatusService.UpdateGoodsStatus(goodsUnit.GoodsId, await kindOfGoodsService.GetGoodsKind(goodsUnit.GoodsId), GoodsStatus.Sold);
            }
            return RedirectToAction("Unit", "Sales", new { saleId });
        }
        catch
        {
            await RestoreCart(saleId, cartService);
            await saleService.CancelSale(saleId);
            return RedirectToAction("Cart", "Sales");
        }
    }

    private async Task RestoreCart(Guid saleId, ICartService cartService)
    {
        string newCartContent = await cartService.MoveGoodsBackToCart(saleId);
        SetNewCartValue(newCartContent);
    }

    /// <summary>
    /// Something went wrong during the payment.
    /// </summary>
    /// <param name="saleId"></param>
    /// <returns></returns>
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ContentResult> SaleCancelling([FromForm] Guid saleId, [FromServices] IExistingSaleManagementService service, [FromServices] ICartService cartService)
    {
        string result;
        try
        {
            // TODO something is wrong here
            await RestoreCart(saleId, cartService);
            await service.CancelSale(saleId);
            result = "Successfully cancelled";
        }
        catch
        {
            result = "Failed to cancel";
        }
        return Content(result);
    }

    [HttpGet("/sales/{saleId:Guid}")]
    public async Task<IActionResult> Unit([FromRoute] Guid saleId, [FromServices] IGetSaleService service, [FromServices] IGetGoodsUnitsOfSaleService goodsService)
    {
        var saleView = await service.GetSaleView(saleId);
        //var goodsItems = automapper.MapToGoodsSearchModels(await goodsService.GetOrigGoodsUnitsOfSale(saleId));
        var goodsItems = await goodsService.GetGoodsUnitsOfSale(saleId);

        var saleModel = new SaleUnitModel(saleView.SaleId, saleView.LocalSaleDate, saleView.LocalReservationDate, saleView.LocalReturningDate, saleView.Status, saleView.Total, (int)saleView.GoodsUnitsCount!, saleView.IsPaid, MapToGoodsList(goodsItems));
        return View(saleModel);
    }

    [HttpGet("/sale/allocate/{saleId:Guid}")]
    public async Task<IActionResult> Allocate([FromRoute] Guid saleId, [FromServices] IGetGoodsUnitsOfSaleService goodsService, [FromServices] IGetSaleService saleService)
    {
        var goodsItems = await goodsService.GetGoodsUnitsOfSale(saleId);
        var sale = await saleService.GetReservation(saleId);
        if (saleService.HasErrors)
        {
            TempData["Errors"] = saleService.Errors.Select(e => e.ErrorMessage).ToArray();
            return RedirectToAction("Unit", new { saleId });
        }
        return View(new AllocateSaleModel(saleId, MapToGoodsList(goodsItems), sale.ReservationExtraInfo!.SecretWord));
    }

    [HttpPost("/sale/allocate")]
    public async Task<IActionResult> Allocate([FromForm] Guid saleId, [FromServices] IExistingSaleManagementService saleService)
    {
        await saleService.UpdateAsNotPaid(saleId);
        if (saleService.HasErrors)
        {
            TempData["Errors"] = saleService.Errors.Select(e => e.ErrorMessage).ToArray();
            return RedirectToAction("Allocate", new { saleId });
        }
        return RedirectToAction("PayForSale", new { saleId });
    }

    [HttpGet("/sale/return/{saleId:Guid}")]
    public async Task<IActionResult> Return([FromRoute] Guid saleId, [FromServices] IGetGoodsUnitsOfSaleService goodsService, [FromServices] IGetSaleService saleService)
    {
        var goodsItems = await goodsService.GetGoodsUnitsOfSale(saleId);
        var sale = await saleService.GetSaleView(saleId);

        ViewBag.Errors = TempData["Errors"];
        return View(new SaleReturnDto(saleId, sale.Total, MapToGoodsList(goodsItems)));
    }

    [HttpPost("/sale/return")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Return([FromForm] SaleReturnModel saleReturnModel, [FromServices] IExistingSaleManagementService saleService)
    {
        await saleService.Return(saleReturnModel);
        if (saleService.HasErrors)
        {
            TempData["Errors"] = saleService.Errors.Select(e => e.ErrorMessage).ToArray();
            return RedirectToAction("Return", new { saleId = saleReturnModel.SaleId });
        }

        return RedirectToAction("Unit", new { saleId = saleReturnModel.SaleId });
    }

}
