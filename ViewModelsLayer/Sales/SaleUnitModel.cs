using DataLayer.SupportClasses;


namespace ViewModelsLayer.Sales;

public record SaleUnitModel(Guid SaleId, DateTime? SaleDate, DateTime? ReservationDate, DateTime? ReturningDate, SaleStatus Status, int Total, int GoodsUnitsCount, bool IsPaid, List<SaleGoodsItemModel> GoodsItems);
