using DataLayer.Common;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.SalesServices;

public interface ISaleManagementService
{
    Task<bool> RegisterSaleAsSold(Guid saleId);
    Task<bool> CancelSale(Guid saleId);
    //Task<bool> DeleteSale(Guid saleId);
}

#warning i'm not sure about this method doesn't have a bizRunner
public class SaleManagementService(MusicalShopDbContext context) : ISaleManagementService
{
    public async Task<bool> CancelSale(Guid saleId)
    {
        try
        {
            var sale = await context.Sales.SingleAsync(sale => sale.SaleId == saleId);
            context.Remove(sale);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    //public Task<bool> DeleteSale(Guid saleId)
    //{

    //}

    public async Task<bool> RegisterSaleAsSold(Guid saleId)
    {
        try
        {
            var sale = await context.Sales.SingleAsync(sale => sale.SaleId == saleId);
            if (sale.IsPaid)
                return false;
            sale.IsPaid = true;
            context.Update(sale);
            await context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
