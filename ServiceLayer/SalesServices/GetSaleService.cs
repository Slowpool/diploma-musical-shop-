using BizLogicBase.Validation;
using DataLayer.Common;
using DataLayer.Models;
using DataLayer.SupportClasses;
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
// TODO it should implement IErrorAdder
public interface IGetSaleService : IErrorAdder
{
    Task<Sale> GetOriginalSale(Guid saleId);
    Task<SaleView> GetSaleView(Guid saleId);
#warning under question
    //Task<SaleSearchDto> GetReadableSale(Guid saleId);
    Task<Sale> GetReservation(Guid saleId);
}
public class GetSaleService(MusicalShopDbContext context) : ErrorAdder, IGetSaleService
{
    public async Task<Sale> GetOriginalSale(Guid saleId)
    {
#warning is there any point to use find here?
        return /*await context.FindAsync<Sale>(saleId) ??*/
            await context.Sales.Include(s => s.MusicalInstruments)
                               .Include(s => s.Accessories)
                               .Include(s => s.SheetMusicEditions)
                               .Include(s => s.AudioEquipmentUnits)
                               .Include(s => s.ReservationExtraInfo)
                               .SingleAsync(s => s.SaleId == saleId)
                               ;
    }

    public async Task<SaleView> GetSaleView(Guid saleId)
    {
        var saleView = await context.SalesView.SingleAsync(sView => sView.SaleId == saleId);
        var sale = await GetOriginalSale(saleId);
        // saleView doesn't have relationships in database, so assign them manually
        saleView.MusicalInstruments = [.. sale.MusicalInstruments];
        saleView.Accessories = [.. sale.Accessories];
        saleView.AudioEquipmentUnits = [.. sale.AudioEquipmentUnits];
        saleView.SheetMusicEditions = [.. sale.SheetMusicEditions];
        return saleView;
    }

#warning under question
    //public async Task<SaleSearchDto> GetReadableSale(Guid saleId)
    //{
    //    var sale = await GetViewSale(saleId);
    //    return new SaleSearchDto(sale.SaleId, sale.LocalDate, sale.Status, sale.Total, sale.PaidBy);
    //}

    public async Task<Sale?> GetReservation(Guid saleId)
    {
        var sale = await GetOriginalSale(saleId);
        if (sale.Status != SaleStatus.Reserved)
        {
            AddError("Продажа не является зарезервированной");
            return null;
        }

        return sale;
    }
}
