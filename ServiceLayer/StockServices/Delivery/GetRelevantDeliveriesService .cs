using DataLayer.Common;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Stock.Delivery;

namespace ServiceLayer.StockServices.Delivery;

public interface IGetRelevantDeliveriesService
{
    Task<GoodsDelivery[]> GetRelevantDeliveries(DeliveryFilterOptions filterOptions, DeliveryOrderByOptions orderByOptions);
}

public class GetRelevantDeliveriesService(MusicalShopDbContext context) : IGetRelevantDeliveriesService
{
    public async Task<GoodsDelivery[]> GetRelevantDeliveries(DeliveryFilterOptions filterOptions, DeliveryOrderByOptions orderByOptions)
    {
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

        Func<GoodsDelivery, DateTimeOffset?> orderByExpression;
        switch (orderByOptions.OrderBy)
        {
            case DeliveryOrderBy.ActualDeliveryDate:
                orderByExpression = gd => gd.ActualDeliveryDate;
                break;
            case DeliveryOrderBy.ExpectedDeliveryDate:
                orderByExpression = gd => gd.ExpectedDeliveryDate;
                break;
        }

#error why
        if (orderByOptions.AscendingOrder)
            query = query.OrderBy(orderByExpression);
        else
            query = query.OrderByDescending(orderByExpression);

    }
}
