using DataLayer.SupportClasses;
using ViewModelsLayer.Goods;


namespace ViewModelsLayer.Sales;

public record SaleUnitModel(Guid SaleId, DateTime? SaleDate, DateTime? ReservationDate, DateTime? ReturningDate, SaleStatus Status, int Total, int GoodsUnitsCount, bool IsPaid, List<GoodsUnitSearchModel> GoodsItems) : ISaleModel;
