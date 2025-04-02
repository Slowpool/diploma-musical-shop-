using DataLayer.SupportClasses;

namespace ViewModelsLayer.Reports;
public record SpecificReportOptions(
    DateTime? FromDate,
    DateTime? ToDate,
    ReportSubtype Subtype,

    KindOfGoods KindOfGoodsForSpecific,
    Guid[] SpecificTypes
    ) : IReportOptions;