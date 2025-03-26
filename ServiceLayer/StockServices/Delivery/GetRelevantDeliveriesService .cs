using BizLogicBase.Validation;
using DataLayer.Common;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.SalesServices.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Common;
using ViewModelsLayer.Stock.Delivery;

namespace ServiceLayer.StockServices.Delivery;

public interface IGetRelevantDeliveriesService : IErrorAdder
{
    Task<List<GoodsDelivery>?> GetRelevantDeliveries(DeliveryFilterOptions filterOptions, DeliveryOrderByOptions orderByOptions, PagingModel pagingModel);
}

public class GetRelevantDeliveriesService(MusicalShopDbContext context) : ErrorAdder, IGetRelevantDeliveriesService
{
    public async Task<List<GoodsDelivery>?> GetRelevantDeliveries(DeliveryFilterOptions filterOptions, DeliveryOrderByOptions orderByOptions, PagingModel pagingModel)
    {
        if (filterOptions.FromActualDeliveryDate > filterOptions.ToActualDeliveryDate)
        {
            // TODO it needs refactoring a little
            AddError("Минимальная дата доставки не может быть больше максимальной");
            return null;
        }

        if (filterOptions.ToExpectedDeliveryDate < filterOptions.FromExpectedDeliveryDate)
        {
            AddError("Минимальная ожидаемая дата доставки не может быть больше максимальной");
            return null;
        }

        IQueryable<GoodsDelivery> query = context.GoodsDeliveries;

        if (filterOptions.IsDelivered)
        {
            query = query.Where(gd => gd.ActualDeliveryDate != null);

            if (filterOptions.FromActualDeliveryDate is not null)
                query = query.Where(gd => gd.ActualDeliveryDate >= filterOptions.FromActualDeliveryDate.LocalToUniversal());

            if (filterOptions.ToActualDeliveryDate is not null)
                query = query.Where(gd => gd.ActualDeliveryDate <= filterOptions.ToActualDeliveryDate.LocalToUniversal());
        }
        else
            query = query.Where(gd => gd.ActualDeliveryDate == null);

        if (filterOptions.FromExpectedDeliveryDate is not null)
            query = query.Where(gd => gd.ExpectedDeliveryDate >= filterOptions.FromExpectedDeliveryDate.LocalToUniversal());

        if (filterOptions.ToExpectedDeliveryDate is not null)
            query = query.Where(gd => gd.ExpectedDeliveryDate <= filterOptions.ToExpectedDeliveryDate.LocalToUniversal());

        Expression<Func<GoodsDelivery, DateTimeOffset?>> orderByExpression = orderByOptions.OrderBy switch
        {
            DeliveryOrderBy.ActualDeliveryDate => gd => gd.ActualDeliveryDate,
            DeliveryOrderBy.ExpectedDeliveryDate => gd => gd.ExpectedDeliveryDate,
            _ => throw new Exception()
        };

        if (orderByOptions.AscendingOrder)
            query = query.OrderBy(orderByExpression);
        else
            query = query.OrderByDescending(orderByExpression);

        query = query.Page(pagingModel);

        return await query.ToListAsync();
    }
}
