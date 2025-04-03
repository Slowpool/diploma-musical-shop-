using DataLayer.SupportClasses;

namespace ViewModelsLayer.Reports;
public record GeneralReportOptions(
    DateTime? FromDate,
    DateTime? ToDate,
    ReportSubtype Subtype,
    ReportChartType ChartType,

    List<KindOfGoods> KindsOfGoods
    ) : IReportOptions
{
    public GeneralReportOptions(ReportCommonOptions commonOptions) : this(commonOptions.FromDate, commonOptions.ToDate, commonOptions.Subtype, commonOptions.ChartType, commonOptions.KindsOfGoodsForGeneral!)
    { }
}
