using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Common;
using ViewModelsLayer.Sales;

namespace ServiceLayer.SalesServices.QueryObjects;
public static class QueryObjectExtensions
{
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int? pageNumber, int? pageSize)
    {
        int pageNumberInner = pageNumber is not null ? (int)pageNumber : Consts.DEFAULT_PAGING_NUMBER;
        int pageSizeInner = pageSize is not null ? (int)pageSize : Consts.DEFAULT_PAGING_SIZE;

        if (pageSizeInner <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size cannot be zero or less than zero.");
        if (pageNumberInner <= 0)
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page number cannot be zero or less than zero.");
        if (pageNumberInner != 1)
            query = query.Skip((pageNumberInner - 1) * pageSizeInner);
        return query.Take(pageSizeInner);
    }

    public static IQueryable<T> Page<T>(this IQueryable<T> query, PagingModel pagingModel)
    {
        return query.Page(pagingModel.PageNumber, pagingModel.PageSize);
    }


    public static IQueryable<SaleView> FilterSalesBy(this IQueryable<SaleView> query, SalesFilterOptions filterOptions)
    {
        string[] types = { "Sale", "Reservation", "Returning" };
        string[] ranges = { "Min", "Max" };
        foreach (string type in types)
            foreach (string range in ranges)
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

        if (filterOptions.Status is not null)
            query = query.Where(sale => sale.Status == filterOptions.Status);
        if (filterOptions.PaidBy is not null)
            query = query.Where(sale => sale.PaidBy == filterOptions.PaidBy);
        return query;
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
