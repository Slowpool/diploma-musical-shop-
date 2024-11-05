using DataLayer.Common;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
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
#warning is there any point to use find here?
        return await context.FindAsync<Sale>(saleId) ??
            await context.Sales.Include(s => s.MusicalInstruments)
                               .Include(s => s.Accessories)
                               .Include(s => s.SheetMusicEditions)
                               .Include(s => s.AudioEquipmentUnits)
                               .SingleAsync(s => s.SaleId == saleId);
    }

    public async Task<SaleView> GetSaleView(Guid saleId)
    {
        var saleView = await context.SalesView.SingleAsync(saleView => saleView.SaleId == saleId);
        var sale = await GetOriginalSale(saleId);
        saleView.MusicalInstruments = [.. sale.MusicalInstruments];
        saleView.Accessories = [.. sale.Accessories];
        saleView.AudioEquipmentUnits= [.. sale.AudioEquipmentUnits];
        saleView.SheetMusicEditions = [.. sale.SheetMusicEditions];
        return saleView;
    }

#warning under question
    //public async Task<SaleSearchDto> GetReadableSale(Guid saleId)
    //{
    //    var sale = await GetViewSale(saleId);
    //    return new SaleSearchDto(sale.SaleId, sale.LocalDate, sale.Status, sale.Total, sale.PaidBy);
    //}
}
