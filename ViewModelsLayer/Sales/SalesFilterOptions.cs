using DataLayer.SupportClasses;

namespace ViewModelsLayer.Sales;
public record class SalesFilterOptions(DateTime? MinSaleDate, DateTime? MaxSaleDate, DateTime? MinReservationDate, DateTime? MaxReservationDate, DateTime? MinReturningDate, DateTime? MaxReturningDate, SaleStatus? Status, SalePaidBy? PaidBy);
