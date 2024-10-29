using DataLayer.SupportClasses;

namespace ViewModelsLayer.Sales;
public record class SalesFilterOptions(DateTime? MinDate, DateTime? MaxDate, SaleStatus Status, SalePaidBy PaidBy);
