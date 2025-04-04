using DataLayer.SupportClasses;

namespace ViewModelsLayer.Reports;
public record SpecificReportOptions(
    DateTime? FromDate,
    DateTime? ToDate,
    ReportSubtype Subtype,
    ReportChartType ChartType,

    KindOfGoods KindOfGoods,
    List<Guid> SpecificTypeIds
    ) : IReportOptions
{
    public SpecificReportOptions(ReportCommonOptions commonOptions) : this(commonOptions.FromDate, commonOptions.ToDate, commonOptions.Subtype, commonOptions.ChartType, (KindOfGoods)commonOptions.KindOfGoodsForSpecific!, commonOptions.SpecificTypes!)
    { }
}