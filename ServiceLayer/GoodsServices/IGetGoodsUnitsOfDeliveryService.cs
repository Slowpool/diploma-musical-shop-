using BizLogicBase.Validation;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using ServiceLayer.StockServices.Delivery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.GoodsServices;

public interface IGetGoodsUnitsOfDeliveryService : IErrorAdder
{
    Task<Dictionary<KindOfGoods, List<Goods>>> GetGoodsUnitsOfDelivery(Guid deliveryId);
}

public class GetGoodsUnitsOfDeliveryService(IGetDeliveryService deliveryService) : ErrorAdder, IGetGoodsUnitsOfDeliveryService
{
    public async Task<Dictionary<KindOfGoods, List<Goods>>> GetGoodsUnitsOfDelivery(Guid deliveryId)
    {
        var delivery = await deliveryService.GetDelivery(deliveryId, true);
        Dictionary<KindOfGoods, List<Goods>> goodsItems = [];
        foreach (var kindOfGoods in Enum.GetValues<KindOfGoods>())
        {
            IEnumerable<Goods> goodsList = kindOfGoods switch
            {
                KindOfGoods.Accessories => delivery.Accessories,
                KindOfGoods.MusicalInstruments => delivery.MusicalInstruments,
                KindOfGoods.SheetMusicEditions => delivery.SheetMusicEditions,
                KindOfGoods.AudioEquipmentUnits => delivery.AudioEquipmentUnits,
                _ => throw new Exception()
            };
            goodsItems[kindOfGoods] = [.. goodsList];
        }
        return goodsItems;
    }
}
