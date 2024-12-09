using BizLogicBase.Common;
using BizLogicBase.Validation;
using BusinessLogicLayer.Sales.Dto;
using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using DbAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Sales;

public class CreateSaleAsNotPaidAction(SalesDbAccess dbAccess) : ErrorAdder, IBizAction<CreateSaleDto, Task<Guid?>>
{
    /// <summary>
    /// When <paramref name="dto"/>.PaidBy is <c>null</c>, this method treat sale as a reservation, otherwise as a sale.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<Guid?> Action(CreateSaleDto dto)
    {
        if (dto.GoodsForSale.Count == 0)
        {
            AddError("Список товаров пуст.");
            return null;
        }

        foreach (var goodsUnit in dto.GoodsForSale)
        {
            ValidateGoodsUnit(goodsUnit);
            goodsUnit.Status = GoodsStatus.AwaitingPayment;
        }

        // TODO fix bug when goods is in cart but not in session cart
        //foreach (var goods in dto.GoodsForSale)
        //{
        //    goods.Status = GoodsStatus.InCart;
        //}

        if (HasErrors)
            return null;

        var sale = new Sale()
        {
            LocalSaleDate = DateTime.UtcNow,
            Status = SaleStatus.YetNotPaid
        };
        dbAccess.CreateSaleAndUpdateGoods(sale, dto.GoodsForSale);
        return sale.SaleId;
    }

    public void ValidateGoodsUnit(Goods goodsUnit)
    {
        if (goodsUnit.Status != GoodsStatus.InCart)
            AddError("В корзину находится товар, статус которого не \"В корзине\"");
        if (goodsUnit.SoftDeleted)
            AddError("В корзину добавлен удаленный товар");
        // TODO load delivery here
// TODO uncomment
        //if (goodsUnit.Delivery?.LocalActualDeliveryDate is null || goodsUnit.ReceiptDate is null)
        //    AddError("В корзину добавлен непоступивший на склад товар");
        if (goodsUnit.Price <= 0)
            AddError("В корзину добавлен товар с некорректной ценой. Цена не может быть меньше или равна 0");
        // if goods unit has a sale, it must be returned. otherwise this loop won't be executed
        // TODO handle reserved sale.
        foreach (var sale in goodsUnit.Sales)
            if (sale.Status != SaleStatus.Returned)
                AddError("В корзину добавлен товар, который входит в другую продажу и не может быть продан");
    }
}
