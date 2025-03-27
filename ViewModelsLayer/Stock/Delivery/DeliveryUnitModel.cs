
using ViewModelsLayer.Goods;

namespace ViewModelsLayer.Stock.Delivery;

public record DeliveryUnitModel(Guid DeliveryId, DateTime? ExpectedDeliveryDate, DateTime? ActualDeliveryDate, bool IsDelivered, List<GoodsUnitSearchModel> GoodsItems);