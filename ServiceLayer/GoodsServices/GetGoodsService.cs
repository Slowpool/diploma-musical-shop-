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
using static Common.ConstValues;

namespace ServiceLayer.GoodsServices;
public interface IGetGoodsService
{
    Task<Goods> GetGoodsInfo(Guid goodsId, KindOfGoods kindOfGoods);
    Task<GoodsUnitSearchDto> GetReadableGoodsInfo(Guid goodsId, KindOfGoods kindOfGoods);
    //Task<Type> GetGoodsType(Guid goodsId);
    Task<KindOfGoods> GetGoodsKind(Guid goodsId);
}
public class GetGoodsService(MusicalShopDbContext context, IMapKindOfGoodsService kindOfGoodsMapper) : IGetGoodsService
{
    public async Task<Goods> GetGoodsInfo(Guid id, KindOfGoods kindOfGoods)
    {
        IQueryable<Goods> goods = kindOfGoodsMapper.MapToSpecificGoods(kindOfGoods);
        return await goods
            // TODO refactoring
            //.Include(g => g.SpecificType)
            .SingleAsync(e => e.GoodsId == id)!;
    }

    public async Task<GoodsUnitSearchDto> GetReadableGoodsInfo(Guid id, KindOfGoods kindOfGoods)
    {
        Goods goods = await GetGoodsInfo(id, kindOfGoods);

        GoodsUnitSearchDto dto = new()
        {
            Id = id.ToString(),
// TODO specific type
            //Type = goods.SpecificType.Name,
            Price = goods.Price
        };

        switch (kindOfGoods)
        {
            case KindOfGoods.MusicalInstruments:
            case KindOfGoods.SheetMusicEditions:
                dynamic specificGoods = goods;
                string from = kindOfGoods == KindOfGoods.MusicalInstruments ? specificGoods.Manufacturer : specificGoods.Author;
                dto.Name = $"{specificGoods.Name} от \"{from}\"";
                dto.Description = $"Год выпуска: {specificGoods.ReleaseYear} {goods.Description}";
                break;
            case KindOfGoods.Accessories:
                var accessory = (Accessory)goods;
                string color = accessory.Color.ToLower();
                string size = accessory.Size.ToLower();
                dto.Name = $"{accessory.Name}, {color}, {size}";
                dto.Description = accessory.Description;
                break;
            case KindOfGoods.AudioEquipmentUnits:
                var audioEquipmentUnit = (AudioEquipmentUnit)goods;
                dto.Name = $"{audioEquipmentUnit.Name}";
                dto.Description = audioEquipmentUnit.Description;
                break;
            default:
                throw new ArgumentException("unknown type");
        };
        return dto;
    }

    //public async Task<Type> GetGoodsType(Guid goodsId)
    //{
    //    throw new NotImplementedException();
    //}

    public async Task<KindOfGoods> GetGoodsKind(Guid goodsId)
    {
        IQueryable<Goods> goods;
        foreach(var kindOfGoods in Enum.GetValues<KindOfGoods>())
        {
            goods = kindOfGoodsMapper.MapToSpecificGoods(kindOfGoods);
            if (goods.Any(g => g.GoodsId == goodsId))
                return kindOfGoods;
        }
        throw new ArgumentException();
    }

    
}
