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

public class ArrangeSaleAction(SalesDbAccess dbAccess) : ErrorAdder, IBizAction<ArrangeSaleDto, Task<Guid?>>
{
    public async Task<Guid?> Action(ArrangeSaleDto dto)
    {
        if (dto.GoodsForSale.Count == 0)
        {
            AddError("Список товаров пуст.");
            return null;
        }
        //foreach(var key in dto.GoodsForSale) { } // guid:type
#warning check for type of goods
#warning check for each goods unit existence in table
#warning check for each goods unit is not in another sale except returned
#warning check for each goods unit
        var sale = new Sale()
        {
            SaleId = Guid.NewGuid(),
            PaidBy = dto.PaidBy,
            SaleDate = DateTimeOffset.UtcNow,
            Status = SaleStatus.Sold
        };
        dbAccess.CreateSale(sale, dto.GoodsForSale);
        return sale.SaleId;
    }
}