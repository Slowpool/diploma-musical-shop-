using DataLayer.SupportClasses;
using ViewModelsLayer.CustomAttributes;

namespace ViewModelsLayer.Reports;
public record ReportGeneralOptionsDto(
    DateTime? FromDate,
    
    DateTime? ToDate,
    
    ReportType Type,
    
    [RequiredWhen(nameof(ReportGeneralOptionsDto.Type), ReportType.General)]
    List<KindOfGoods>? KindsOfGoodsForGeneral,

    [RequiredWhen(nameof(ReportGeneralOptionsDto.Type), ReportType.SpecificGoods)]
    KindOfGoods? KindOfGoodsForSpecific,

    [RequiredWhen(nameof(ReportGeneralOptionsDto.Type), ReportType.SpecificGoods)]
    Dictionary<KindOfGoods, Dictionary<Guid, string>>? SpecificTypes

    );