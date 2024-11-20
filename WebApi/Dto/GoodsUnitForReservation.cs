using DataLayer.SupportClasses;

namespace WebApi.Dto;

public record class GoodsUnitForReservation(Guid goodsId, KindOfGoods kindOfGoods);