
using DataLayer.NotMapped;
using DataLayer.SupportClasses;

namespace BusinessLogicLayer.Sales.Dto;

public record class CreateSaleDto(List<Goods> GoodsForSale);
