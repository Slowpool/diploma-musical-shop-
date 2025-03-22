using DataLayer.Models;
using DataLayer.SupportClasses;

namespace ViewModelsLayer.Sales;

public record class SaleSearchModel(Guid SaleId, DateTime? ReservationDate, DateTime? SaleDate, DateTime? ReturningDate, SaleStatus Status, int Total, SalePaidBy? PaidBy, List<SaleGoodsItemModel> GoodsItems);
