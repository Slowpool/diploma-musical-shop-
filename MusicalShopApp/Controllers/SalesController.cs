using Common;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.SalesServices;
using ViewModelsLayer.Sales;

namespace MusicalShopApp.Controllers
{
    [Authorize(Roles = CommonNames.SellerRole)]
    public class SalesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Search(string q, [FromServices] IGetRelevantSalesService service, DateTime? minDate, DateTime? maxDate, SaleStatus status=SaleStatus.Sold, SalePaidBy paidBy=SalePaidBy.Cash, SalesOrderBy orderBy=SalesOrderBy.Relevance, bool orderByAscending=true)
        {
            var filterOptions = new SalesFilterOptions(minDate, maxDate, status, paidBy);
            var orderByOptions = new SalesOrderByOptions(orderBy, orderByAscending);
            List<SaleSearchDto> list = await service.GetRelevantSales(q, filterOptions, orderByOptions);
            return View(new SalesSearchModel(q, list, list.Count, filterOptions, orderByOptions));
        }


    }
}
