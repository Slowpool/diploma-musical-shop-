
namespace ViewModelsLayer.Stock.Delivery;

public enum DeliveryOrderBy
{
    /// <summary>
    /// Random sorting, strictly speaking.
    /// </summary>
    Relevance,
    ActualDeliveryDate,
    ExpectedDeliveryDate,
}