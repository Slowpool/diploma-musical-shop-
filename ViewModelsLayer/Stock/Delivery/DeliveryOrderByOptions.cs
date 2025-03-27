
namespace ViewModelsLayer.Stock.Delivery;
public record DeliveryOrderByOptions(DeliveryOrderBy OrderBy = DeliveryOrderBy.Relevance, bool AscendingOrder = true);