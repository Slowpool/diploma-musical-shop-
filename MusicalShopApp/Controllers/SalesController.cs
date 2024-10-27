using Common;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModelsLayer.Sales;

namespace MusicalShopApp.Controllers
{
    [Authorize(Roles = CommonNames.SellerRole)]
    public class SalesController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Search(string q, DateTime? minDate, DateTime? maxDate, SaleStatus status=SaleStatus.Sold, PaidBy paidBy=PaidBy.Cash)
        {
            List<SaleSearchDto> list = [];
            return View(new SalesSearchModel(q, list, list.Count, new SaleFilterOptions(minDate, maxDate, status, paidBy), new SaleOrderByOptions()));
        }


    }
}
