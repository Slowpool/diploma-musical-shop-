using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Goods;
using static Common.Consts;

namespace ServiceLayer.GoodsServices;
public interface IGetGoodsService
{
    Task<Goods> GetOrigGoodsItem(Guid goodsId, KindOfGoods kindOfGoods, bool includeRelations = false);
    Task<GoodsUnitSearchModel> GetReadableGoodsInfo(Guid goodsId, KindOfGoods kindOfGoods);
    //Task<Type> GetGoodsType(Guid goodsId);
}
public class GetGoodsService(IMapKindOfGoodsService kindOfGoodsMapper) : IGetGoodsService
{
    public async Task<Goods> GetOrigGoodsItem(Guid id, KindOfGoods kindOfGoods, bool includeRelations = false)
    {
        IQueryable<Goods> goods = kindOfGoodsMapper.MapToSpecificGoods(kindOfGoods);
        if (includeRelations)
            goods = goods.Include(g => g.Delivery)
                         .Include(g => g.Sales)
                         // duck typing :{
                         .Include("SpecificType");

        return await goods
            .SingleAsync(e => e.GoodsId == id)!;
    }

    public async Task<GoodsUnitSearchModel> GetReadableGoodsInfo(Guid id, KindOfGoods kindOfGoods)
    {
        dynamic goodsItem = await GetOrigGoodsItem(id, kindOfGoods, true);

        GoodsUnitSearchModel dto = new()
        {
            GoodsId = id,
            SpecificType = goodsItem.SpecificType.Name,
            Price = goodsItem.Price,
            KindOfGoods = kindOfGoods,
            Status = goodsItem.Status,
        };

        switch (kindOfGoods)
        {
            case KindOfGoods.MusicalInstruments:
            case KindOfGoods.SheetMusicEditions:
                dynamic specificGoods = goodsItem;
                string from = kindOfGoods == KindOfGoods.MusicalInstruments ? specificGoods.Manufacturer : specificGoods.Author;
                dto.Name = $"{specificGoods.Name} от \"{from}\"";
#warning violation. it must be in view
                dto.Description = $"Год выпуска: {specificGoods.ReleaseYear}. {goodsItem.Description}";
                break;
            case KindOfGoods.Accessories:
                var accessory = (Accessory)goodsItem;
                string color = accessory.Color.ToLower();
                string size = accessory.Size.ToLower();
                dto.Name = $"{accessory.Name}, {color}, {size}";
                dto.Description = accessory.Description;
                break;
            case KindOfGoods.AudioEquipmentUnits:
                var audioEquipmentUnit = (AudioEquipmentUnit)goodsItem;
                dto.Name = $"{audioEquipmentUnit.Name}";
                dto.Description = audioEquipmentUnit.Description;
                break;
            default:
                throw new ArgumentException("unknown type");
        }
        ;
        return dto;
    }

    //public async Task<Type> GetGoodsType(Guid goodsId)
    //{
    //    throw new NotImplementedException();
    //}


}
