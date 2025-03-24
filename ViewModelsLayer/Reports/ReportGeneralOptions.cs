using DataLayer.SupportClasses;
using ViewModelsLayer.CustomAttributes;

namespace ViewModelsLayer.Reports;
public record ReportGeneralOptions(
    DateTime? FromDate,
    
    DateTime? ToDate,
    
    ReportType Type,
    
    [RequiredWhen(nameof(ReportGeneralOptions.Type), ReportType.General)]
    List<KindOfGoods>? KindsOfGoodsForGeneral,

    [RequiredWhen(nameof(ReportGeneralOptions.Type), ReportType.SpecificGoods)]
    KindOfGoods? KindOfGoodsForSpecific,

    [RequiredWhen(nameof(ReportGeneralOptions.Type), ReportType.SpecificGoods)]
    Dictionary<KindOfGoods, List<SpecificType>>? SpecificTypes

    );