using DataLayer.SupportClasses;
using ViewModelsLayer.Sales;

namespace ViewModelsLayer.Goods;
public record class GoodsUnitModel(
    Guid GoodsId,
    KindOfGoods KindOfGoods,
    string Name,
    int Price,
    GoodsStatus Status,
    string? Description,
    /*int ReleaseYear, string Manufacturer, ManufacturerType ManufacturerType,*/
    string SpecificTypeName,
    DateTime? ReceiptDate,
    Guid? DeliveryId,
    List<SaleSearchModel> Sales) : IGoodsModelAddableInCart;
