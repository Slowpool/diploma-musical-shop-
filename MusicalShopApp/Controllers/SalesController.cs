using Common;
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
    public async Task<IActionResult> Search(string q, [FromServices] IGetRelevantSalesService service, DateTime? minDate, DateTime? maxDate, SaleStatus status=SaleStatus.Sold, SalePaidBy paidBy=SalePaidBy.Cash, SalesOrderBy orderBy=SalesOrderBy.Relevance, bool orderByAscending=true)
    {
        var filterOptions = new SalesFilterOptions(minDate, maxDate, status, paidBy);
        var orderByOptions = new SalesOrderByOptions(orderBy, orderByAscending);
        List<SaleSearchDto> list = await service.GetRelevantSales(q, filterOptions, orderByOptions);
        return View(new SalesSearchModel(q, list, list.Count, filterOptions, orderByOptions));
    }

    [HttpPost]
    public async Task<IActionResult> ArrangeSale([FromServices] IArrangeSaleService service, [FromQuery] SalePaidBy paidBy)
    {
        Dictionary<Guid, Type> goods = [];
#warning don't like this yellow underlining line
        foreach(var goodsIdAndType in GoodsIdsAndTypes)
        {
            goods[Guid.Parse(GetGoodsId(goodsIdAndType))] = GetGoodsType(goodsIdAndType);
        }
        Guid? saleId = await service.ArrangeSale(goods, paidBy);
        if (!service.HasErrors)
#warning redirect to payment and then to successfully sold sale
            return RedirectToAction("PayForSale", saleId);
        else
            return RedirectToAction("Cart", "Goods", new SaleErrorModel(service.Errors));
    }

    public async Task<IActionResult> PayForSale(Guid saleId)
    {
        return View(saleId);
    }

    /// <summary>
    /// Payment was successful.
    /// </summary>
    /// <param name="saleId"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ContentResult> RegistrationOfSaleAsSold(Guid saleId, [FromServices] ISaleManagementService service)
    {

    }

    /// <summary>
    /// Something went wrong during the payment.
    /// </summary>
    /// <param name="saleId"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ContentResult> SaleCancelling(Guid saleId, [FromServices] ISaleManagementService service)
    {
        return await service.CancelSale(saleId) ? Content("Successfully cancelled") : Content("Failed to cancel");
    }
}
