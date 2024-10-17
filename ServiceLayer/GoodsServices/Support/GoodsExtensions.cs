using DataLayer.Models.SupportClasses;
using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ServiceLayer.GoodsServices.Support;

public static class GoodsExtensions
{
#warning i'm not sure about it
    public static IQueryable<T> OrderGoodsBy<T>(this IQueryable<T> goods, GoodsOrderBy goodsOrderBy)
        where T : Goods
    {
        return goodsOrderBy switch
        {
            GoodsOrderBy.Relevance => goods,
            GoodsOrderBy.PriceAscending => goods.OrderBy(g => g.Price),
            GoodsOrderBy.PriceDescending => goods.OrderByDescending(g => g.Price),
            GoodsOrderBy.ReceiptDateAscending => goods.OrderBy(g => g.ReceiptDate),
            GoodsOrderBy.ReceiptDateDescending => goods.OrderByDescending(g => g.ReceiptDate),
            _ => throw new ArgumentOutOfRangeException(
                            nameof(GoodsOrderBy), goodsOrderBy, null),
        };
    }
}
