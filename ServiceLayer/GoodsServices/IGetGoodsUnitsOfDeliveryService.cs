using BizLogicBase.Validation;
using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.GoodsServices;

public interface IGetGoodsUnitsOfDeliveryService : IErrorAdder
{
    Task<List<Goods>> GetGoodsUnitsOfDeliveryService(Guid deliveryId);
}

public class GetGoodsUnitsOfDeliveryService : ErrorAdder, IGetGoodsUnitsOfDeliveryService
{
    public async Task<List<Goods>> GetGoodsUnitsOfDeliveryService(Guid deliveryId)
    {

    }
}
