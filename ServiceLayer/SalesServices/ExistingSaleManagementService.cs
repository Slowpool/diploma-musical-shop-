using DataLayer.Common;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.SalesServices;

public interface IExistingSaleManagementService
{
    Task RegisterSaleAsPaid(Guid saleId);
    Task CancelSale(Guid saleId);
}

public class ExistingSaleManagementService(MusicalShopDbContext context) : IExistingSaleManagementService
{
    public async Task CancelSale(Guid saleId)
    {
        var sale = await context.Sales.SingleAsync(sale => sale.SaleId == saleId);
        //TODO soft deleting?
        context.Remove(sale);
        await context.SaveChangesAsync();
    }

    public async Task RegisterSaleAsPaid(Guid saleId)
    {
        var sale = await context.Sales.SingleAsync(sale => sale.SaleId == saleId);
        if (sale.IsPaid)
            throw new Exception("attempt to register an already paid sale as a paid one");
        sale.IsPaid = true;
        context.Update(sale);
        await context.SaveChangesAsync();
    }
}
