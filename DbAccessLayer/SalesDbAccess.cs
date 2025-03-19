using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLayer;

public class SalesDbAccess(MusicalShopDbContext context)
{
    public async Task CreateSaleAndUpdateGoods(Sale sale, List<Goods> goods)
    {
        foreach(var goodsUnit in goods)
        {
            goodsUnit.Sales.Add(sale);
            context.Update(goodsUnit);
#warning what if goods were passed here with edited values except goods.Status? UPD: okey, here it's safety, but probably dto with restricted data for update would be better. after all, that's a purpose of dto
#warning do i need it at all? what if the latest line of this method may execute updating?
            //context.Update(goodsUnit);
        }
        await context.AddAsync(sale);
    }

    public async Task CreateReservationAndUpdateGoods(Sale sale, List<Goods> goods, string secretWord)
    {
        sale.ReservationExtraInfo = new()
        {
            Sale = sale,
            SecretWord = secretWord,
        };

        foreach (var goodsUnit in goods)
        {
            goodsUnit.Sales.Add(sale);
            context.Update(goodsUnit);
        }
        await context.AddAsync(sale);
    }
}
