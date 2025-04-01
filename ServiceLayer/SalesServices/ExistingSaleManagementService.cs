using BizLogicBase.Validation;
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
using ViewModelsLayer.Sales;

namespace ServiceLayer.SalesServices;

public interface IExistingSaleManagementService : IErrorAdder
{
    Task RegisterSaleAsPaid(Guid saleId, SalePaidBy paidBy);
    Task CancelSale(Guid saleId);
    Task Return(SaleReturnModel model);
    Task UpdateAsNotPaid(Guid saleId);
}

public class ExistingSaleManagementService(MusicalShopDbContext context, IGetSaleService getSaleService) : ErrorAdder, IExistingSaleManagementService
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
        sale.LocalSaleDate = DateTime.Now;

        context.Update(sale);
        await context.SaveChangesAsync();
    }

    public async Task Return(SaleReturnModel model)
    {
        if (model.CustomerConfirmation && model.RefundConfirmation)
        {
            var sale = await getSaleService.GetOriginalSale(model.SaleId);
            if (sale.Status != SaleStatus.Sold)
            {
                AddError("Эта продажа не может быть возвращена т.к. она не имеет статуса \"Продано\"");
                return;
            }
            sale.Status = SaleStatus.Returned;
            sale.LocalReturningDate = DateTime.Now;
            context.Update(sale);
            await context.SaveChangesAsync();
        }
        else
        {
            if (!model.CustomerConfirmation)
                AddError("Требуется подтверждение покупателя для оформления возврата");
            if (!model.RefundConfirmation)
                AddError("Требуется подтверждение выдачи покупателю денег");
        }
    }

    public async Task UpdateAsNotPaid(Guid saleId)
    {
        var sale = await getSaleService.GetOriginalSale(saleId);
        // TODO validation

        sale.Status = SaleStatus.YetNotPaid;
        context.Update(sale);
        await context.SaveChangesAsync();
    }
}
