using DataLayer.SupportClasses;

namespace ViewModelsLayer.Sales;

public record class SaleSearchDto(Guid SalesId, DateTime Date, SaleStatus Status, int Total, PaidBy PaidBy);
