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
#warning awful
        if (filterOptions.MinSaleDate != null)
            query = query.Where(sale => sale.LocalSaleDate >= filterOptions.MinSaleDate);
        if (filterOptions.MaxSaleDate != null)
            query = query.Where(sale => sale.LocalSaleDate <= filterOptions.MaxSaleDate);

		if (filterOptions.MinReturningDate != null)
			query = query.Where(sale => sale.LocalReturningDate >= filterOptions.MinReturningDate);
		if (filterOptions.MaxReturningDate != null)
			query = query.Where(sale => sale.LocalReturningDate <= filterOptions.MaxReturningDate);

		if (filterOptions.MinReservationDate != null)
			query = query.Where(sale => sale.LocalReservationDate >= filterOptions.MinReservationDate);
		if (filterOptions.MaxReservationDate != null)
			query = query.Where(sale => sale.LocalReservationDate <= filterOptions.MaxReservationDate);

		return query.Where(sale => sale.Status == filterOptions.Status
                                && sale.PaidBy == filterOptions.PaidBy);
    }

    public static IQueryable<SaleView> OrderBy(this IQueryable<SaleView> query, SalesOrderByOptions orderByOptions)
    {
        Expression<Func<SaleView, object>> selector = orderByOptions.OrderBy switch
        {
            SalesOrderBy.Relevance => sale => sale.SaleId,
            SalesOrderBy.SaleDate => sale => sale.SaleDate,
            SalesOrderBy.ReservationDate => sale => sale.ReservationDate,
            SalesOrderBy.ReturningDate => sale => sale.ReturningDate,
            SalesOrderBy.GoodsUnitsCount => sale => sale.GoodsUnitsCount,
            _ => throw new Exception()
        };
        return orderByOptions.AscendingOrder ? query.OrderBy(selector) : query.OrderByDescending(selector);
    }


}
