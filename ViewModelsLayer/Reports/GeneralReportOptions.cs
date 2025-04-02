using DataLayer.SupportClasses;

namespace ViewModelsLayer.Reports;
public record GeneralReportOptions(
    DateTime? FromDate,
    DateTime? ToDate,
    ReportSubtype Subtype,

    KindOfGoods[] KindsOfGoodsForGeneral
    ) : IReportOptions;
