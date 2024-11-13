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
    Task<Type> GetGoodsType(string goodsId);
    Task<Goods> GetGoodsInfo(string id, KindOfGoods kindOfGoods);
    Task<GoodsUnitSearchDto> GetReadableGoodsInfo(string id, KindOfGoods kindOfGoods);
    static abstract dynamic GetSpecificGoodsByKind(MusicalShopDbContext context, KindOfGoods kindOfGoods);
}
public class GetGoodsService(MusicalShopDbContext context) : IGetGoodsService
{
    public async Task<Goods> GetGoodsInfo(string id, KindOfGoods kindOfGoods)
    {
        IQueryable<Goods> goods = GetGoodsService.GetSpecificGoodsByKind(context, kindOfGoods);
        return await goods
            //.Include(g => g.SpecificType)
            .SingleAsync(e => e.GoodsId.ToString() == id)!;
    }

    public async Task<GoodsUnitSearchDto> GetReadableGoodsInfo(string id, KindOfGoods kindOfGoods)
    {
        Goods goods = await GetGoodsInfo(id, kindOfGoods);

        GoodsUnitSearchDto dto = new()
        {
            Id = id,
// TODO specific type
            //Type = goods.SpecificType.Name,
            Price = goods.Price
        };

        switch (kindOfGoods)
        {
            // I know that handling these things this way is a bit wrong, but come+on. they are so similar-alike.
            case KindOfGoods.MusicalInstruments:
            case KindOfGoods.SheetMusicEditions:
                dynamic specificGoods = goods;
                string from = kindOfGoods == KindOfGoods.MusicalInstruments ? specificGoods.Manufacturer : specificGoods.Author;
                dto.Name = $"{specificGoods.Name} от \"{from}\"";
                string description = $"Год выпуска: {specificGoods.ReleaseYear}. ";
                //int remainedLength = MAX_LENGTH_OF_BRIEF_GOODS_DESCRIPTION - description.Length;
                //int takeDescriptionLength = specificGoods.Description.Length <= remainedLength ? specificGoods.Description.Length : remainedLength;
                //description += specificGoods.Description.Substring(0, takeDescriptionLength);
                //if (takeDescriptionLength < specificGoods.Description.Length)
                //    description += "...";
                dto.Description = description;
                break;
            case KindOfGoods.Accessories:
                var accessory = (Accessory)goods;
                string color = accessory.Color.ToLower();
                string size = accessory.Size.ToLower();
                dto.Name = $"{accessory.Name}, {color}, {size}";
                dto.Description = accessory.Description;//[..MAX_LENGTH_OF_BRIEF_GOODS_DESCRIPTION];
                break;
            case KindOfGoods.AudioEquipmentUnits:
                var audioEquipmentUnit = (AudioEquipmentUnit)goods;
                dto.Name = $"{audioEquipmentUnit.Name}";
                dto.Description = audioEquipmentUnit.Description;// [..MAX_LENGTH_OF_BRIEF_GOODS_DESCRIPTION];
                break;
            default:
                throw new ArgumentException("unknown type");
        };
        return dto;
    }

    public async Task<Type> GetGoodsType(string goodsId)
    {
        Guid guid = Guid.Parse(goodsId);
#warning dijkstra claimed that function should have only one output
        if (await context.Accessories.ContainsAsync(new Accessory() { GoodsId = guid }))
            return typeof(Accessory);
        else if (await context.MusicalInstruments.ContainsAsync(new MusicalInstrument() { GoodsId = guid }))
            return typeof(MusicalInstrument);
        else if (await context.AudioEquipmentUnits.ContainsAsync(new AudioEquipmentUnit() { GoodsId = guid }))
            return typeof(AudioEquipmentUnit);
        else if (await context.SheetMusicEditions.ContainsAsync(new SheetMusicEdition() { GoodsId = guid }))
            return typeof(SheetMusicEdition);
        else
            throw new Exception();
    }

    public static dynamic GetSpecificGoodsByKind(MusicalShopDbContext context, KindOfGoods kindOfGoods)
        => kindOfGoods switch
        {
            KindOfGoods.Accessories => context.Accessories,
            KindOfGoods.AudioEquipmentUnits => context.AudioEquipmentUnits,
            KindOfGoods.MusicalInstruments => context.MusicalInstruments,
            KindOfGoods.SheetMusicEditions => context.SheetMusicEditions,
            _ => throw new Exception()
        };
}
