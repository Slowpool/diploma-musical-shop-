using Common;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        string[] types = { "Sale", "Reservation", "Returning" };
        string[] ranges = { "Min", "Max" };
        foreach(string type in types)
            foreach(string range in ranges)
            {
                PropertyInfo someDate = typeof(SalesFilterOptions).GetProperty($"{range}{type}Date")!;
                var currentDate = (DateTime?)someDate.GetValue(filterOptions);
                if (currentDate != null)
                {
                    DateTimeOffset? newDateTimeOffset = currentDate.LocalToUniversal();
                    var parameter = Expression.Parameter(typeof(SaleView));
                    var property = Expression.Property(parameter, $"{type}Date");
                    var constantExpression = Expression.Constant(newDateTimeOffset, typeof(DateTimeOffset?));
                    var equalityExpression = Expression.GreaterThanOrEqual(property, constantExpression);
                    var predicate = (Expression<Func<SaleView, bool>>)Expression.Lambda(equalityExpression, parameter);
                    query = query.Where(predicate);
                    
                }
            }
//#warning awful. I see solution with reflexion.
//        if (filterOptions.MinSaleDate != null)
//        {
//            var newDateTimeOffset = filterOptions.MinSaleDate.LocalToUniversal();
//            query = query.Where(sale => sale.SaleDate >= newDateTimeOffset);
//        }
//        if (filterOptions.MaxSaleDate != null)
//        {
//            var newDateTimeOffset = filterOptions.MaxSaleDate.LocalToUniversal();
//            query = query.Where(sale => sale.SaleDate <= newDateTimeOffset);
//        }

//		if (filterOptions.MinReturningDate != null)
//			query = query.Where(sale => sale.LocalReturningDate >= filterOptions.MinReturningDate);
//		if (filterOptions.MaxReturningDate != null)
//			query = query.Where(sale => sale.LocalReturningDate <= filterOptions.MaxReturningDate);

//		if (filterOptions.MinReservationDate != null)
//			query = query.Where(sale => sale.LocalReservationDate >= filterOptions.MinReservationDate);
//		if (filterOptions.MaxReservationDate != null)
//			query = query.Where(sale => sale.LocalReservationDate <= filterOptions.MaxReservationDate);

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
