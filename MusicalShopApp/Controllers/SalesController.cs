using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicalShopApp.Controllers.BaseControllers;
using ServiceLayer.GoodsServices;
using ServiceLayer.SalesServices;
using ViewModelsLayer.Goods;
using ViewModelsLayer.Sales;

namespace MusicalShopApp.Controllers;

[Authorize(Policy = nameof(CommonNames.Seller))]
public class SalesController : CartViewerBaseController
{
    [HttpGet]
    public async Task<IActionResult> Search(string q, [FromServices] IGetRelevantSalesService service, DateTime? minSaleDate, DateTime? maxSaleDate, DateTime? minReservationDate, DateTime? maxReservationDate, DateTime? minReturningDate, DateTime? maxReturningDate, SalePaidBy? paidBy, SaleStatus? status, SalesOrderBy orderBy=SalesOrderBy.Relevance, bool orderByAscending=true)
    {
        var filterOptions = new SalesFilterOptions(minSaleDate, maxSaleDate, minReservationDate, maxReservationDate, minReturningDate, maxReturningDate, status, paidBy);
        var orderByOptions = new SalesOrderByOptions(orderBy, orderByAscending);
        List<SaleSearchDto> list = await service.GetRelevantSales(q, filterOptions, orderByOptions);
        return View(new SalesSearchModel(q, list, list.Count, filterOptions, orderByOptions));
    }

    [HttpPost("/sale/arrange")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateSaleAsNotSold([FromServices] ICartService cartService, [FromServices] ICreateSaleService createSaleService)
    {
        var goods = await cartService.GetGoodsFromCart(GoodsIdsAndKinds);
        Guid? saleId = await createSaleService.CreateSaleAsNotPaid(goods);
        if (!createSaleService.HasErrors)
        {
            ClearSessionCart();
            return RedirectToAction("PayForSale", new { saleId });
        }
        else
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

            ViewBag.ReservationCreated = TempData["ReservationCreated"] = true;
            ViewBag.ReservationId = TempData["ReservationId"] = reservationId;
            return Redirect(Url.Action("Cart", "Goods")!);
        }
        else
            // TODO flash stuff
            return RedirectToAction("Cart", "Goods");//, new SaleErrorModel(service.Errors));
    }

    [HttpGet]
    public async Task<IActionResult> PayForSale([FromQuery] Guid saleId)
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
    public async Task<ContentResult> RegisterSaleAsSold(Guid saleId, SalePaidBy paidBy, [FromServices] IExistingSaleManagementService saleService, [FromServices] ICartService cartService, [FromServices] IGetGoodsUnitsRelatedToSaleService goodsService, [FromServices]  IUpdateGoodsStatusService goodsStatusService, [FromServices] IMapKindOfGoodsService kindOfGoodsService)
    {
        string result;
        try
        {
            await saleService.RegisterSaleAsPaid(saleId, paidBy);
            var goods = await goodsService.GetOrigGoodsUnitsRelatedToSale(saleId);
            foreach(var goodsUnit in goods)
            {
                await goodsStatusService.UpdateGoodsStatus(goodsUnit.GoodsId, await kindOfGoodsService.GetGoodsKind(goodsUnit.GoodsId), GoodsStatus.Sold);
            }
            result = "Successfully registered";
        }
        catch
        {
            await RestoreCart(saleId, cartService);
            await saleService.CancelSale(saleId);
            result = "Failed to register";
        }
        return Content(result);
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

    [HttpGet("/goods/{saleId}")]
    public async Task<IActionResult> SaleUnit([FromRoute] Guid saleId, [FromServices] IGetSaleService service)
    {
        var saleView = await service.GetSaleView(saleId);
        // guitar here is a latch
        var goodsModel = new SaleUnitModel(saleView.saleId, saleView.Name, saleView.Price, saleView.Status, saleView.Description, "Guitar", saleView.ReceiptDate);
        return View(goodsModel);
    }
}   
