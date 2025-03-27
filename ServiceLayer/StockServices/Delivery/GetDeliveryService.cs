using BizLogicBase.Validation;
using DataLayer.Common;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.StockServices.Delivery;

public interface IGetDeliveryService : IErrorAdder
{
    public Task<bool> Exists(Guid deliveryId);
    public Task<GoodsDelivery> GetDelivery(Guid deliveryId);
}

public class GetDeliveryService(MusicalShopDbContext context) : ErrorAdder, IGetDeliveryService
{
    public async Task<bool> Exists(Guid deliveryId)
    {
        return await context.GoodsDeliveries.AnyAsync(e => e.GoodsDeliveryId == deliveryId);
    }

    public async Task<GoodsDelivery> GetDelivery(Guid deliveryId)
    {
        return await context.GoodsDeliveries.SingleAsync(e => e.GoodsDeliveryId == deliveryId);
    }
}
