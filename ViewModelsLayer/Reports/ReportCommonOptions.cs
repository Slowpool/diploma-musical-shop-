using DataLayer.SupportClasses;
using ViewModelsLayer.CustomAttributes;

namespace ViewModelsLayer.Reports;
public record ReportCommonOptions(
    DateTime? FromDate,
    
    DateTime? ToDate,
    
    ReportType Type,
    ReportSubtype Subtype,
    ReportChartType ChartType,
    
    [RequiredWhen(nameof(ReportCommonOptions.Type), ReportType.General)]
    List<KindOfGoods>? KindsOfGoodsForGeneral,

    [RequiredWhen(nameof(ReportCommonOptions.Type), ReportType.SpecificGoods)]
    KindOfGoods? KindOfGoodsForSpecific,

    [RequiredWhen(nameof(ReportCommonOptions.Type), ReportType.SpecificGoods)]
    List<Guid>? SpecificTypes

    );