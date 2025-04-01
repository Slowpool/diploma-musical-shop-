using BizLogicBase.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Stock.Delivery;

namespace ServiceLayer.StockServices.Delivery;

public interface IAcceptDeliveryService : IErrorAdder
{
    async Task AcceptDelivery(AcceptDeliveryModel model);
}

public class AcceptDeliveryService : ErrorAdder
{
    public async Task AcceptDelivery(AcceptDeliveryModel model)
    {
        if (!model.Confirmation)
        {
            AddError("Требуется подтверждение получения всех товаров из доставки");
            return;
        }


    }
}
