using GoodsClass = DataLayer.NotMapped.Goods;

namespace ViewModelsLayer.Reports.ExtensionMethods;
public static class ReportBuilder
{
    public static int Select(this IQueryable<GoodsClass> query, ReportSubtype subtype, ReportChartType chartType)
    {
        // TODO

        //return query.Select();
        return Random.Shared.Next(1, 10);
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
