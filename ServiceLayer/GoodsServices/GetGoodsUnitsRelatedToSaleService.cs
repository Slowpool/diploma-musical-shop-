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
public class GetGoodsUnitsRelatedToSaleService(IGetSaleService saleService) : IGetGoodsUnitsRelatedToSaleService
{
    public async Task<List<Goods>> GetGoodsUnitsRelatedToSale(Guid saleId)
    {
        var sale = await saleService.GetOriginalSale(saleId);
        return sale.MusicalInstruments
                   .Cast<Goods>()
                   .Concat(sale.Accessories
                               .Cast<Goods>())
                   .Concat(sale.SheetMusicEditions
                               .Cast<Goods>())
                   .Concat(sale.AudioEquipmentUnits
                               .Cast<Goods>())
                   .ToList();
    }
}
