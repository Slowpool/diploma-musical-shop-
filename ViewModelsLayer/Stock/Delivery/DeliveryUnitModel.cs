
using ViewModelsLayer.Goods;

namespace ViewModelsLayer.Stock.Delivery;

public record DeliveryUnitModel(Guid GoodsDeliveryId, DateTime? ExpectedDeliveryDate, DateTime? ActualDeliveryDate, bool IsDelivered, List<GoodsUnitSearchModel> GoodsItems) : IDeliveryModel;