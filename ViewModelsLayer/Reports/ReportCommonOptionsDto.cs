using DataLayer.SupportClasses;

namespace ViewModelsLayer.Reports;
public record ReportCommonOptionsDto(
    DateTime? FromDate,
    DateTime? ToDate,
    ReportSubtype? Subtype,
    ReportType? Type,
    ReportChartType? ChartType,
    List<KindOfGoods> KindsOfGoodsForGeneral,
    KindOfGoods? KindOfGoodsForSpecific,
    Dictionary<KindOfGoods, Dictionary<Guid, string>> SpecificTypes
    );