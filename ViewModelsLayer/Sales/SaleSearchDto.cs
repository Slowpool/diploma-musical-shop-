using DataLayer.Models;
using DataLayer.SupportClasses;

namespace ViewModelsLayer.Sales;

public record class SaleSearchDto(Guid SalesId, DateTime? ReservationDate, DateTime? SaleDate, DateTime? ReturningDate, SaleStatus Status, int Total, SalePaidBy? PaidBy, List<string> BriefGoodsDescriptions);
