using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
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
    Task<Goods> GetGoodsInfo<T>(string id)
        where T : Goods, new();
    Task<Type> GetGoodsType(string goodsId);
    Task<Goods> GetGoodsInfo(string id, Type type);
    Task<GoodsUnitSearchDto> GetReadableGoodsInfo<T>(string id)
        where T : Goods, new();

    Task<GoodsUnitSearchDto> GetReadableGoodsInfo(string id, Type type);
}
public class GetGoodsService(MusicalShopDbContext context) : IGetGoodsService
{
    public async Task<Goods> GetGoodsInfo<T>(string id)
        where T : Goods, new()
    {
        IQueryable<Goods>? targetGoods = new T() switch
        {
            MusicalInstrument => context.MusicalInstruments,
            Accessory => context.Accessories,
            AudioEquipmentUnit => context.AudioEquipmentUnits,
            SheetMusicEdition => context.SheetMusicEditions,
            _ => throw new ArgumentException("unknown type")
        };
        return (T)await targetGoods
            // TODO specific type
            //.Include(g => g.SpecificType)
            .SingleAsync(e => e.GoodsId.ToString() == id)!;
    }

    public async Task<GoodsUnitSearchDto> GetReadableGoodsInfo<T>(string id)
        where T : Goods, new()
    {
        Goods goods = await GetGoodsInfo<T>(id);

        GoodsUnitSearchDto dto = new()
        {
            Id = id,
// TODO specific type
            //Type = goods.SpecificType.Name,
            Price = goods.Price
        };

        switch (typeof(T).Name)
        {
            // I know that handling these things this way is a bit wrong, but come+on. they are so similar-alike.
            case "MusicalInstrument":
            case "SheetMusicEdition":
                dynamic specificGoods = goods;
                string from = typeof(T).Name == "MusicalInstrument" ? specificGoods.Manufacturer : specificGoods.Author;
                dto.Name = $"{specificGoods.Name} от \"{from}\"";
                string description = $"Год выпуска: {specificGoods.ReleaseYear}. ";
                //int remainedLength = MAX_LENGTH_OF_BRIEF_GOODS_DESCRIPTION - description.Length;
                //int takeDescriptionLength = specificGoods.Description.Length <= remainedLength ? specificGoods.Description.Length : remainedLength;
                //description += specificGoods.Description.Substring(0, takeDescriptionLength);
                //if (takeDescriptionLength < specificGoods.Description.Length)
                //    description += "...";
                dto.Description = description;
                break;
            case "Accessory":
                var accessory = (Accessory)goods;
                string color = accessory.Color.ToLower();
                string size = accessory.Size.ToLower();
                dto.Name = $"{accessory.Name}, {color}, {size}";
                dto.Description = accessory.Description;//[..MAX_LENGTH_OF_BRIEF_GOODS_DESCRIPTION];
                break;
            case "AudioEquipmentUnit":
                var audioEquipmentUnit = (AudioEquipmentUnit)goods;
                dto.Name = $"{audioEquipmentUnit.Name}";
                dto.Description = audioEquipmentUnit.Description;// [..MAX_LENGTH_OF_BRIEF_GOODS_DESCRIPTION];
                break;
            default:
                throw new ArgumentException("unknown type");
        };
        return dto;

    }

    /// <summary>
    /// This method redirect to <see cref="GetReadableGoodsInfo{T}"/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<GoodsUnitSearchDto> GetReadableGoodsInfo(string id, Type type)
    {
        MethodInfo methodInfo = typeof(GetGoodsService).GetMethod("GetReadableGoodsInfo", BindingFlags.IgnoreReturn | BindingFlags.Public | BindingFlags.Instance, [typeof(string)])!;
        methodInfo = methodInfo.MakeGenericMethod(type);

        return await (Task<GoodsUnitSearchDto>)methodInfo.Invoke(this, [id])!;
    }

    public async Task<Type> GetGoodsType(string goodsId)
    {
        Guid guid = Guid.Parse(goodsId);
#warning why dijkstra said that function should have only one output
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

    public async Task<Goods> GetGoodsInfo(string id, Type type)
    {
        var methodInfo = typeof(GetGoodsService).GetMethod("GetGoodsInfo", BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreReturn, [typeof(string)]);
        return await (Task<Goods>)methodInfo.MakeGenericMethod(type).Invoke(this, [id]);
    }
}
