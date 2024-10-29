using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
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
public class GetGoodsUnitsRelatedToSaleService(MusicalShopDbContext context) : IGetGoodsUnitsRelatedToSaleService
{
    public async Task<List<Goods>> GetGoodsUnitsRelatedToSale(Guid saleId)
    {
        List<Goods> result = [];
#warning i can do better
        result.AddRange(context.MusicalInstruments.Where(mi => mi.SaleId == saleId));
        result.AddRange(context.Accessories.Where(a => a.SaleId == saleId));
        result.AddRange(context.SheetMusicEditions.Where(sme => sme.SaleId == saleId));
        result.AddRange(context.AudioEquipmentUnits.Where(aeu => aeu.SaleId == saleId));
        return result;
    }
}
