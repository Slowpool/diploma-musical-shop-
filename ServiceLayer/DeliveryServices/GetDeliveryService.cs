using BizLogicBase.Validation;
using DataLayer.Common;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DeliveryServices;

public interface IGetDeliveryService : IErrorAdder
{
    public Task<bool> Exists(Guid deliveryId);
}

public class GetDeliveryService(MusicalShopDbContext context) : ErrorAdder, IGetDeliveryService
{
    public async Task<bool> Exists(Guid deliveryId)
    {
        return await context.GoodsDeliveries.AnyAsync(e => e.GoodsDeliveryId == deliveryId);
    }
}
