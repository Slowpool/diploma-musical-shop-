using DataLayer.SupportClasses;
using ViewModelsLayer.CustomAttributes;

namespace ViewModelsLayer.Reports;
public record ReportCommonOptions(
    DateTime? FromDate,
    
    DateTime? ToDate,
    
    ReportType Type,
    ReportSubtype Subtype,
    
    [RequiredWhen(nameof(ReportCommonOptions.Type), ReportType.General)]
    KindOfGoods[]? KindsOfGoodsForGeneral,

    [RequiredWhen(nameof(ReportCommonOptions.Type), ReportType.SpecificGoods)]
    KindOfGoods? KindOfGoodsForSpecific,

    [RequiredWhen(nameof(ReportCommonOptions.Type), ReportType.SpecificGoods)]
    Guid[]? SpecificTypes

    );