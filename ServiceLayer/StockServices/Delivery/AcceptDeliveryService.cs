using BizLogicBase.Validation;
using DataLayer.Common;
using DataLayer.Models;
using ServiceLayer.GoodsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Stock.Delivery;

namespace ServiceLayer.StockServices.Delivery;

public interface IAcceptDeliveryService : IErrorAdder
{
    Task AcceptDelivery(AcceptDeliveryModel model);
}

public class AcceptDeliveryService(MusicalShopDbContext context, IGetDeliveryService deliveryService, IGetGoodsUnitsOfDeliveryService goodsService) : ErrorAdder, IAcceptDeliveryService
{
    public async Task AcceptDelivery(AcceptDeliveryModel model)
    {
        if (!model.Confirmation)
        {
            AddError("Требуется подтверждение получения всех товаров из доставки");
            return;
        }

        GoodsDelivery delivery;
        try
        {
            delivery = await deliveryService.GetDelivery(model.DeliveryId);
        }
        catch (InvalidOperationException)
        {
            AddError("Данная доставка не найдена");
            return;
        }

        if (delivery.ActualDeliveryDate is not null)
        {
            AddError("Данная доставка уже прибыла");
            return;
        }

        delivery.LocalActualDeliveryDate = DateTime.Now;
        var goodsItems = (await goodsService.GetGoodsUnitsOfDelivery(delivery.GoodsDeliveryId)).Values.SelectMany(list => list);

        foreach (var goodsItem in goodsItems)
        {
            goodsItem.ReceiptDate = delivery.ActualDeliveryDate;
            context.Update(goodsItem);
        }

        context.Update(delivery);
        await context.SaveChangesAsync();

    }
}
