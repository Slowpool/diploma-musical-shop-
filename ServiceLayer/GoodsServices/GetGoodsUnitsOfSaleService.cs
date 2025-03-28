using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.SalesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Sales;

namespace ServiceLayer.GoodsServices;
public interface IGetGoodsUnitsOfSaleService
{
    Task<List<Goods>> GetOrigGoodsUnitsOfSale(Guid saleId);
    Task<List<SaleGoodsItemModel>> GetGoodsModelsOfSale(Guid saleId);
    Task<Dictionary<KindOfGoods, List<Goods>>> GetGoodsUnitsOfSale(Guid saleId);
}

public class GetGoodsUnitsOfSaleService(IGetSaleService saleService, IMapKindOfGoodsService mapKindOfGoodsService) : IGetGoodsUnitsOfSaleService
{
    public async Task<List<Goods>> GetOrigGoodsUnitsOfSale(Guid saleId)
    {
        var sale = await saleService.GetOriginalSale(saleId);
        return [..sale.MusicalInstruments.Cast<Goods>(),
                ..sale.Accessories.Cast<Goods>(),
                ..sale.SheetMusicEditions.Cast<Goods>(),
                ..sale.AudioEquipmentUnits.Cast<Goods>()
                ];
    }

    public async Task<List<SaleGoodsItemModel>> GetGoodsModelsOfSale(Guid saleId)
    {
        List<Goods> relatedGoods = await GetOrigGoodsUnitsOfSale(saleId);
        List<SaleGoodsItemModel> goodsItemModels = [];
        foreach (Goods goodsUnit in relatedGoods)
        {
            goodsItemModels.Add(new(goodsUnit.GoodsId, await mapKindOfGoodsService.GetGoodsKind(goodsUnit.GoodsId), goodsUnit.Name));
        }
        return goodsItemModels;
    }

    // TODO delivery service copypast
    public async Task<Dictionary<KindOfGoods, List<Goods>>> GetGoodsUnitsOfSale(Guid saleId)
    {
        var sale = await saleService.GetOriginalSale(saleId);
        Dictionary<KindOfGoods, List<Goods>> goodsItems = [];
        foreach (var kindOfGoods in Enum.GetValues<KindOfGoods>())
        {
            IEnumerable<Goods> goodsList = kindOfGoods switch
            {
                KindOfGoods.Accessories => sale.Accessories,
                KindOfGoods.MusicalInstruments => sale.MusicalInstruments,
                KindOfGoods.SheetMusicEditions => sale.SheetMusicEditions,
                KindOfGoods.AudioEquipmentUnits => sale.AudioEquipmentUnits,
                _ => throw new Exception()
            };
            goodsItems[kindOfGoods] = [.. goodsList];
        }
        return goodsItems;
    }
}
