using BizLogicBase.Common;
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

public class CreateSaleAction(SalesDbAccess dbAccess) : ErrorAdder, IBizAction<CreateSaleDto, Task<Guid?>>
{
    public async Task<Guid?> Action(CreateSaleDto dto)
    {
        if (dto.GoodsForSale.Count == 0)
        {
            AddError("Список товаров пуст.");
            return null;
        }
        //foreach(var key in dto.GoodsForSale) { } // guid:type
        // actually everything below marked as warned is validation.
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
            Status = SaleStatus.Sold
        };
        dbAccess.CreateSale(sale, dto.GoodsForSale);
        return sale.SaleId;
    }
}
