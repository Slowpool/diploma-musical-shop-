using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicalShopApp.Controllers.BaseControllers;
using ServiceLayer.SalesServices;
using ViewModelsLayer.Sales;

namespace MusicalShopApp.Controllers;

[Authorize(Roles = CommonNames.SellerRole)]
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
    public async Task<IActionResult> CreateSaleAsNotSold([FromForm] SalePaidBy? paidBy, [FromServices] ICreateSaleService createSaleService, [FromServices] ICartService cartService)
    {
        var goods = await cartService.GetGoodsFromCart(GoodsIdsAndKinds);
        Guid? saleId = await createSaleService.CreateSaleAsNotPaid(goods, paidBy);
        if (!createSaleService.HasErrors)
        {
            ClearCart();
            return RedirectToAction("PayForSale", new { saleId });
        }
        else
            return RedirectToAction("Cart", "Goods");//, new SaleErrorModel(service.Errors));
    }

    [HttpPost("/sale/reserve")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Reserve([FromServices] ICartService cartService)
    {
        return View();
    }

    private void ClearCart()
    {
        SetNewCartValue(string.Empty);
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
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ContentResult> RegisterSaleAsSold(Guid saleId, [FromServices] IExistingSaleManagementService saleService, [FromServices] ICartService cartService)
    {
        string result;
        try
        {
            await saleService.RegisterSaleAsPaid(saleId);
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
}   
