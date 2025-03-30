using DataLayer.SupportClasses;

namespace ViewModelsLayer.Goods;
public record class GoodsUnitModel(
    Guid Guid,
    KindOfGoods KindOfGoods,
    string Name,
    int Price,
    GoodsStatus Status,
    string? Description,
    /*int ReleaseYear, string Manufacturer, ManufacturerType ManufacturerType,*/
    string SpecificTypeName,
    DateTime? ReceiptDate);
