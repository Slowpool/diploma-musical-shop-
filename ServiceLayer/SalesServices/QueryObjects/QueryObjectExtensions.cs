using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Sales;

namespace ServiceLayer.SalesServices.QueryObjects;
public static class QueryObjectExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        if (pageSize <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize cannot be zero or less than zero.");
        if (pageNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "pageNum cannot be zero or less than zero.");
        if (pageNumber != 1)
            query = query.Skip((pageNumber - 1) * pageSize);
        return query.Take(pageSize);
    }

    public static IQueryable<SaleView> FilterBy(this IQueryable<SaleView> query, SalesFilterOptions filterOptions)
    {
        if (filterOptions.MinDate != null)
            query = query.Where(sale => sale.LocalDate >= filterOptions.MinDate);
        if (filterOptions.MaxDate != null)
            query = query.Where(sale => sale.LocalDate <= filterOptions.MaxDate);
        return query.Where(sale => sale.Status == filterOptions.Status
                                && sale.PaidBy == filterOptions.PaidBy);
    }

    public static IQueryable<SaleView> OrderBy(this IQueryable<SaleView> query, SalesOrderByOptions orderByOptions)
    {
        Expression<Func<SaleView, object>> selector;
        switch(orderByOptions.OrderBy)
        {
            case SalesOrderBy.Relevance:
                selector = sale => sale.SaleId;
                break;
            case SalesOrderBy.Date:
                selector = sale => sale.LocalDate;
                break;
            case SalesOrderBy.GoodsUnitsCount:
                selector = sale => sale.GoodsUnitsCount;
                break;
            default:
                throw new Exception();
        }
        if (orderByOptions.AscendingOrder)
            return query.OrderBy(selector);
        else
            return query.OrderByDescending(selector);
    }


}
