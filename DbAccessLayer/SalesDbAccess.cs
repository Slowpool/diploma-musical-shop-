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
    public void CreateSale(Sale sale, List<Goods> goods)
    {
        foreach(var goodsUnit in goods)
        {
            goodsUnit.Sales.Add(sale);
#warning do i need it?
            context.Update(goodsUnit);
        }
        context.Add(sale);
    }
}
