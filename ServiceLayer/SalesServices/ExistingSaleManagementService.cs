using DataLayer.Common;
using DataLayer.Models;
using DataLayer.SupportClasses;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.GoodsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.SalesServices;

public interface IExistingSaleManagementService
{
    Task RegisterSaleAsPaid(Guid saleId, SalePaidBy paidBy);
    Task CancelSale(Guid saleId);
}

public class ExistingSaleManagementService(MusicalShopDbContext context) : IExistingSaleManagementService
{
    public async Task CancelSale(Guid saleId)
    {
        var sale = await context.Sales.SingleAsync(sale => sale.SaleId == saleId);
        sale.SoftDeleted = true;
        // TODO also soft delte all goods_sale linking tables
        await context.SaveChangesAsync();
    }

    public async Task RegisterSaleAsPaid(Guid saleId, SalePaidBy paidBy)
    {
        var sale = await context.Sales.SingleAsync(sale => sale.SaleId == saleId);
        if (sale.IsPaid)
            throw new ArgumentException("attempt to register an already paid sale as a paid one");
        // TODO via service??
        sale.IsPaid = true;
        sale.PaidBy = paidBy;
        sale.Status = SaleStatus.Sold;
        context.Update(sale);
        await context.SaveChangesAsync();
    }
}
