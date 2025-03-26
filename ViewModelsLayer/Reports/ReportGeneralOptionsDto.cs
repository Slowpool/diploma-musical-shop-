using DataLayer.SupportClasses;
using ViewModelsLayer.CustomAttributes;

namespace ViewModelsLayer.Reports;
public record ReportGeneralOptionsDto(
    DateTime? FromDate,
    
    DateTime? ToDate,
    
    ReportType Type,
    ReportSubtype Subtype,
    
    [RequiredWhen(nameof(ReportGeneralOptionsDto.Type), ReportType.General)]
    KindOfGoods[]? KindsOfGoodsForGeneral,

    [RequiredWhen(nameof(ReportGeneralOptionsDto.Type), ReportType.SpecificGoods)]
    KindOfGoods? KindOfGoodsForSpecific,

    [RequiredWhen(nameof(ReportGeneralOptionsDto.Type), ReportType.SpecificGoods)]
    Guid[]? SpecificTypes

    );