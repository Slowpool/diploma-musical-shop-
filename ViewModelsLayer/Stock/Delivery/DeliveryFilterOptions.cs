
namespace ViewModelsLayer.Stock.Delivery;

public record DeliveryFilterOptions(DateTime? FromActualDeliveryDate, DateTime? ToActualDeliveryDate, DateTime? FromExpectedDeliveryDate, DateTime? ToExpectedDeliveryDate, TernaryChoice IsDelivered = TernaryChoice.Any);