using DataLayer.SupportClasses;

namespace ViewModelsLayer.Sales;

public record SaleGoodsItemModel(Guid GoodsId, KindOfGoods KindOfGoods, string Name);

