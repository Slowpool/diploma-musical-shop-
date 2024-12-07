﻿using BizLogicBase.Common;
using BizLogicBase.Validation;
using BusinessLogicLayer.Sales.Dto;
using DataLayer.Common;
using DataLayer.Models;
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
#warning already checked before an Action calling
        if (dto.GoodsForSale.Count == 0)
        {
            AddError("Список товаров пуст.");
            return null;
        }



        foreach(var goodsUnit in dto.GoodsForSale)
        {
            goodsUnit.Status = GoodsStatus.AwaitingPayment;
        }

        // TODO fix bug when goods is in cart but not in session cart
        //foreach(var key in dto.GoodsForSale) { } // guid:type
        // actually everything below marked with warned is validation.
#warning check for type of goods
#warning check for each goods unit existence in table
#warning check for each goods unit is not in another sale except returned
#warning check for each goods unit status
        //foreach (var goods in dto.GoodsForSale)
        //{
        //    goods.Status = GoodsStatus.InCart;
        //}
        var sale = new Sale()
        {
            SaleId = Guid.NewGuid(),
            PaidBy = dto.PaidBy,
            LocalSaleDate = DateTime.UtcNow,
            Status = SaleStatus.YetNotPaid
        };
        dbAccess.CreateSale(sale, dto.GoodsForSale);
        return sale.SaleId;
    }
}
