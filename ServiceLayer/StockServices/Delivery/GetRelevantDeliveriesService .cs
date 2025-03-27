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
    public async Task<List<GoodsDelivery>?> GetRelevantDeliveries(DeliveryFilterOptions filter, DeliveryOrderByOptions orderBy, PagingModel pagingModel)
    {
        if (filter.FromActualDeliveryDate > filter.ToActualDeliveryDate)
        {
            // TODO it needs refactoring a little
            AddError("Минимальная дата получения доставки не может быть больше максимальной");
            return null;
        }

        if (filter.ToExpectedDeliveryDate < filter.FromExpectedDeliveryDate)
        {
            AddError("Минимальная ожидаемая дата получения доставки не может быть больше максимальной");
            return null;
        }

        IQueryable<GoodsDelivery> query = context.GoodsDeliveries;

        switch (filter.IsDelivered)
        {
            case TernaryChoice.Any:
                if (filter.FromActualDeliveryDate is not null)
                    query = query.Where(gd => gd.ActualDeliveryDate == null || gd.ActualDeliveryDate >= filter.FromActualDeliveryDate.LocalToUniversal());
                if (filter.ToActualDeliveryDate is not null)
                    query = query.Where(gd => gd.ActualDeliveryDate == null || gd.ActualDeliveryDate <= filter.ToActualDeliveryDate.LocalToUniversal());
                break;

            case TernaryChoice.True:
                query = query.Where(gd => gd.ActualDeliveryDate != null);
                if (filter.FromActualDeliveryDate is not null)
                    query = query.Where(gd => gd.ActualDeliveryDate >= filter.FromActualDeliveryDate.LocalToUniversal());
                if (filter.ToActualDeliveryDate is not null)
                    query = query.Where(gd => gd.ActualDeliveryDate <= filter.ToActualDeliveryDate.LocalToUniversal());
                break;

            case TernaryChoice.False:
                query = query.Where(gd => gd.ActualDeliveryDate == null);
                break;
        }

        if (filter.FromExpectedDeliveryDate is not null)
            query = query.Where(gd => gd.ExpectedDeliveryDate >= filter.FromExpectedDeliveryDate.LocalToUniversal());

        if (filter.ToExpectedDeliveryDate is not null)
            query = query.Where(gd => gd.ExpectedDeliveryDate <= filter.ToExpectedDeliveryDate.LocalToUniversal());

        Expression<Func<GoodsDelivery, dynamic>> orderByExpression = orderBy.OrderBy switch
        {
            DeliveryOrderBy.ActualDeliveryDate => gd => gd.ActualDeliveryDate,
            DeliveryOrderBy.ExpectedDeliveryDate => gd => gd.ExpectedDeliveryDate,
            DeliveryOrderBy.Relevance => gd => gd.GoodsDeliveryId,
            _ => throw new Exception()
        };

        if (orderBy.AscendingOrder)
            query = query.OrderBy(orderByExpression);
        else
            query = query.OrderByDescending(orderByExpression);

        query = query.Page(pagingModel);

        return await query.ToListAsync();
    }
}
