using DataLayer.SupportClasses;

namespace ViewModelsLayer.Reports;
public record ReportGeneralOptionsModel(
    DateTime FromDate,
    DateTime ToDate,
    ReportType? Type,
    List<KindOfGoods> KindsOfGoodsForGeneral,
    KindOfGoods? KindOfGoodsForSpecific,
    Dictionary<KindOfGoods, Dictionary<Guid, string>> SpecificTypes
    );