using Azure;
using BusinessLogicLayer.Goods;
using BusinessLogicLayer.Goods.Dto;
using DataLayer.Common;
using static Common.ConstValues;
using DataLayer.Models;
using DataLayer.Models.SupportClasses;
using DataLayer.NotMapped;
using DbAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ServiceLayer.GoodsServices;

#warning i ain't wanna create service for each action
public class GoodsService(MusicalShopDbContext context)
{
#warning i don't like this method. strictly speaking, i see this> (T?)(Goods?) cast for oithe first time
    public async Task<T?> GetGoodsInfo<T>(string id)
        where T : Goods
        => typeof(T).Name switch
        {
            "MusicalInstrument" => (T?)(Goods?)await context.MusicalInstruments.SingleOrDefaultAsync(e => e.MusicalInstrumentId.ToString() == id),
            "Accessory" => (T?)(Goods?)await context.Accessories.SingleOrDefaultAsync(e => e.AccessoryId.ToString() == id)!,
            "AudioEquipmentUnit" => (T?)(Goods?)await context.AudioEquipmentUnits.SingleOrDefaultAsync(e => e.AudioEquipmentUnitId.ToString() == id)!,
            "SheetMusicEdition" => (T?)(Goods?)await context.SheetMusicEditions.SingleOrDefaultAsync(e => e.SheetMusicEditionId.ToString() == id)!,
            _ => null,
        };

    public async Task<GoodsUnitSearchDto?> GetReadableGoodsInfo<T>(string id)
        where T : Goods
    {
        Goods? goods = await GetGoodsInfo<T>(id);
        if (goods == null)
            return null;

        GoodsUnitSearchDto dto = new()
        {
            Id = id,
            Type = goods.Type.Name,
            Price = goods.Price
        };


        switch (typeof(T).Name)
        {
// I know that to handle it in such a way is little wrong, but come+on. they are so similar-alike.
            case "MusicalInstrument":
            case "SheetMusicEdition":
                dynamic specificGoods = goods;
                string from = typeof(T).Name == "MusicalInstrument" ? specificGoods.Manufacturer : specificGoods.Author;
                dto.Name = $"{specificGoods.Type.Name} от {from}";
                string description = $"Год выпуска: {specificGoods.ReleaseYear}.";
                description = description + specificGoods.Description[..(MAX_LENGTH_OF_BRIEF_GOODS_DESCRIPTION - description.Length)];
                dto.Description = description;
                break;
            case "Accessory":
                var accessory = (Accessory)goods;
                string color = accessory.Color.ToLower();
                string size = accessory.Size.ToLower();
                dto.Name = $"{accessory.Type.Name}, {color}, {size}";
                dto.Description = accessory.Description[..MAX_LENGTH_OF_BRIEF_GOODS_DESCRIPTION];
                break;
            case "AudioEquipmentUnit":
                var audioEquipmentUnit = (AudioEquipmentUnit)goods;
                dto.Name = $"{audioEquipmentUnit.Type.Name}";
                dto.Description = audioEquipmentUnit.Description[..MAX_LENGTH_OF_BRIEF_GOODS_DESCRIPTION];
                break;
#warning Unreachable code
            default:
                return null;
        };
        return dto;

    }

    public async Task<GoodsUnitSearchDto?> GetReadableGoodsInfo(string id, Type type)
    {
        MethodInfo methodInfo = typeof(GoodsService).GetMethod("", BindingFlags.Public, [typeof(string)])!;
        methodInfo = methodInfo.MakeGenericMethod(type);

        var del = methodInfo.CreateDelegate<Func<string, Task<GoodsUnitSearchDto?>>>();
        return await del(id);
    }


    // Kinda complex task to implement.
    public async Task<Dictionary<string, Type>> GetRelevantGoodsIds(string researchText, GoodsFilter filterEnum, GoodsOrderBy orderByEnum, int page, int pageSize)
    {
        // latch
        return new Dictionary<string, Type>();
    }
}
