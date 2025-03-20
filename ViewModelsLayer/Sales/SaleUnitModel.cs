using DataLayer.SupportClasses;


namespace ViewModelsLayer.Sales;

public record SaleUnitModel(Guid SaleId, DateTimeOffset? SaleDate, DateTimeOffset? ReservationDate, DateTimeOffset? ReturningDate, SaleStatus Status, int Total, int GoodsUnitsCount, bool IsPaid, Dictionary<string, string> GoodsItems);

#error use it
