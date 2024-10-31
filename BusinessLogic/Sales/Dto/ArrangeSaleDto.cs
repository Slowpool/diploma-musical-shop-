
using DataLayer.NotMapped;
using DataLayer.SupportClasses;

namespace BusinessLogicLayer.Sales.Dto;

public record class ArrangeSaleDto(List<Goods> GoodsForSale, SalePaidBy PaidBy);
