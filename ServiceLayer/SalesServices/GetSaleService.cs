using DataLayer.Common;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Sales;

namespace ServiceLayer.SalesServices;
public interface IGetSaleService
{
    Task<Sale> GetOriginalSale(Guid saleId);
    Task<SaleView> GetSaleView(Guid saleId);
#warning under question
    //Task<SaleSearchDto> GetReadableSale(Guid saleId);
}
public class GetSaleService(MusicalShopDbContext context) : IGetSaleService
{
    public async Task<Sale> GetOriginalSale(Guid saleId)
    {
        return await context.FindAsync<Sale>(saleId);
    }

    public async Task<SaleView> GetSaleView(Guid saleId)
    {
#warning how to throw exception here
        return await context.FindAsync<SaleView>(saleId);
    }

#warning under question
    //public async Task<SaleSearchDto> GetReadableSale(Guid saleId)
    //{
    //    var sale = await GetViewSale(saleId);
    //    return new SaleSearchDto(sale.SaleId, sale.LocalDate, sale.Status, sale.Total, sale.PaidBy);
    //}
}
