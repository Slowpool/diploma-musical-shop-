using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq.Expressions;
using GoodsClass = DataLayer.NotMapped.Goods;

namespace ViewModelsLayer.Reports.ExtensionMethods;
public static class ReportBuilder
{
    public async static Task<int> Select(this IQueryable<GoodsClass> query, ReportSubtype subtype)
    {
        // TODO
        int result;
        switch (subtype)
        {
            case ReportSubtype.NumberOfSales:
                query = query.Where(g => g.Status == GoodsStatus.Sold);
                result = await query.CountAsync();
                break;
            case ReportSubtype.AverageSalesSpeed:
                query = query.Where(g => g.Status == GoodsStatus.Sold && g.ReceiptDate != null);
                // TODO not trivial stuff. select the `saleDate - receiptDate` and then take an average
                var items = query.Include(g => g.Sales)
                                   .SelectMany(g => g.Sales, (g, s) => new { g.ReceiptDate, s.SaleDate })
                                   .ToList();
                var listOfDays = items
                                   .Where(ae => ae.SaleDate != null)
#error what's wrong?
                                   .Select(ae => (ae.ReceiptDate - ae.SaleDate)!.Value.Days)
                                   ;
                result = (int)listOfDays.Average();
                break;
            default:
                throw new Exception();
        }
        return result;
    }

    // TODO this method may work incorrectly for some specific types of report (which does not exist for now)
    public static IQueryable<GoodsClass> Where(this IQueryable<GoodsClass> query, DateTime? fromDate, DateTime? toDate)
    {
        if (fromDate is not null)
            query = query.Where(g => g.ReceiptDate >= fromDate);

        if (toDate is not null)
            query = query.Where(g => g.ReceiptDate <= toDate);

        return query;
    }
}
