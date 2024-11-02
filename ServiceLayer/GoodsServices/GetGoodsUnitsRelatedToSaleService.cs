using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.SalesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.GoodsServices;
public interface IGetGoodsUnitsRelatedToSaleService
{
    Task<List<Goods>> GetGoodsUnitsRelatedToSale(Guid saleId);
}
public class GetGoodsUnitsRelatedToSaleService(MusicalShopDbContext context, IGetSaleService saleService) : IGetGoodsUnitsRelatedToSaleService
{
    public async Task<List<Goods>> GetGoodsUnitsRelatedToSale(Guid saleId)
    {
        var sale = await saleService.GetOriginalSale(saleId);
        return sale.MusicalInstruments.Concat<Goods>(sale.Accessories).Concat(sale.SheetMusicEditions).Concat(sale.AudioEquipmentUnits).ToList();
    }
}
