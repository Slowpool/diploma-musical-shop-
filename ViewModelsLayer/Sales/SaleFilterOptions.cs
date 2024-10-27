using DataLayer.SupportClasses;

namespace ViewModelsLayer.Sales;
public record class SaleFilterOptions(DateTime? MinDate, DateTime? MaxDate, SaleStatus Status, PaidBy PaidBy);
