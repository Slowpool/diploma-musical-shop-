using Azure;
using BusinessLogicLayer.Goods;
using BusinessLogicLayer.Goods.Dto;
using DataLayer.Common;
using static Common.ConstValues;
using Common;
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
using ServiceLayer.GoodsServices.Support;

namespace ServiceLayer.GoodsServices;

#warning i ain't wanna create service for each action
public class GoodsService(MusicalShopDbContext context)
{
#warning i don't like this method. strictly speaking, i see this> (T?)(Goods?) cast for oithe first time
    public async Task<T?> GetGoodsInfo<T>(string id)
        where T : Goods
        => typeof(T).Name switch
        {
            "MusicalInstrument" => (T?)(Goods?)await context.MusicalInstruments.SingleOrDefaultAsync(e => e.GoodsId.ToString() == id),
            "Accessory" => (T?)(Goods?)await context.Accessories.SingleOrDefaultAsync(e => e.GoodsId.ToString() == id)!,
            "AudioEquipmentUnit" => (T?)(Goods?)await context.AudioEquipmentUnits.SingleOrDefaultAsync(e => e.GoodsId.ToString() == id)!,
            "SheetMusicEdition" => (T?)(Goods?)await context.SheetMusicEditions.SingleOrDefaultAsync(e => e.GoodsId.ToString() == id)!,
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
    public async Task<List<KeyValuePair<string, Type>>> GetRelevantGoodsIds(string researchText, GoodsFilterOptions filterOptions, GoodsOrderBy orderByEnum, int page, int pageSize)
    {
        var suitableGoods = new List<Goods>();
        switch (filterOptions.GoodsFilter)
        {
            // I added paging for each table here to reduce in-memory size of data.
            // Then I page them again. At first, there's collection of 15 + 15 + 15 + 15 = 60 rows,
            // than 15 rows paging happens again.
#warning it can be implemented via generic avoiding copy-past
            case GoodsFilter.None:
                suitableGoods.AddRange(context.Accessories
                                       .Where(a => a.Description.Contains(researchText))
                                       .OrderGoodsBy<Goods>(orderByEnum)
#warning i screw up with paging.
                                       .Page(page, pageSize)
                                       .ToList());
                suitableGoods.AddRange(context.MusicalInstruments
                                       .Where(a => a.Description.Contains(researchText))
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());
                suitableGoods.AddRange(context.AudioEquipmentUnits
                                       .Where(a => a.Description.Contains(researchText))
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());
                suitableGoods.AddRange(context.SheetMusicEditions
                                       .Where(a => a.Description.Contains(researchText))
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());

                break;
            case GoodsFilter.Type:
                string[] types = filterOptions.FilterValue.Split(',');
                if (types.Length == 0)
                    throw new ArgumentException("goods filter value was empty");
                foreach (string type in types)
                {
                    switch (type.ToLower())
                    {
                        case "accessories":
                            suitableGoods.AddRange(context.Accessories
                                       .Where(a => a.Description.Contains(researchText))
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());
                            break;
                        case "musicalinstruments":
                            suitableGoods.AddRange(context.MusicalInstruments
                                                   .Where(a => a.Description.Contains(researchText))
                                                   .OrderGoodsBy<Goods>(orderByEnum)
                                                   .Page(page, pageSize)
                                                   .ToList());
                            break;
                        case "audioequipmentunits":
                            suitableGoods.AddRange(context.AudioEquipmentUnits
                                                   .Where(a => a.Description.Contains(researchText))
                                                   .OrderGoodsBy<Goods>(orderByEnum)
                                                   .Page(page, pageSize)
                                                   .ToList());
                            break;
                        case "sheetmusiceditions":
                            suitableGoods.AddRange(context.SheetMusicEditions
                                                   .Where(a => a.Description.Contains(researchText))
                                                   .OrderGoodsBy<Goods>(orderByEnum)
                                                   .Page(page, pageSize)
                                                   .ToList());
                            break;
                        default:
                            throw new ArgumentException("unknown goods filter value");
                    }
                }
                break;
            case GoodsFilter.ReceiptDate:
                string[] dates = filterOptions.FilterValue.Split(',');
                DateTime fromDate = DateTime.Parse(dates[0]);
                DateTime toDate = DateTime.Parse(dates[1]);
                suitableGoods.AddRange(context.Accessories
                                       .Where(a => a.Description.Contains(researchText)
                                           && a.ReceiptDate >= fromDate
                                           && a.ReceiptDate <= toDate)
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());
                suitableGoods.AddRange(context.MusicalInstruments
                                       .Where(a => a.Description.Contains(researchText)
                                           && a.ReceiptDate >= fromDate
                                           && a.ReceiptDate <= toDate)
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());
                suitableGoods.AddRange(context.AudioEquipmentUnits
                                       .Where(a => a.Description.Contains(researchText)
                                           && a.ReceiptDate >= fromDate
                                           && a.ReceiptDate <= toDate)
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());
                suitableGoods.AddRange(context.SheetMusicEditions
                                       .Where(a => a.Description.Contains(researchText)
                                           && a.ReceiptDate >= fromDate
                                           && a.ReceiptDate <= toDate)
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());
                break;
            case GoodsFilter.ReleaseYear:
                string[] years = filterOptions.FilterValue.Split(',');
                int fromYear = int.Parse(years[0]);
                int toYear = int.Parse(years[0]);
                suitableGoods.AddRange(context.Accessories
                                       .Where(a => a.Description.Contains(researchText)
                                       && a.ReceiptDate != null
                                       && a.ReceiptDate.Value.Year >= fromYear
                                           && a.ReceiptDate.Value.Year <= toYear)
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());
                suitableGoods.AddRange(context.MusicalInstruments
                                       .Where(a => a.Description.Contains(researchText)
                                       && a.ReceiptDate != null
                                       && a.ReceiptDate.Value.Year >= fromYear
                                           && a.ReceiptDate.Value.Year <= toYear)
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());
                suitableGoods.AddRange(context.AudioEquipmentUnits
                                       .Where(a => a.Description.Contains(researchText)
                                       && a.ReceiptDate != null
                                       && a.ReceiptDate.Value.Year >= fromYear
                                           && a.ReceiptDate.Value.Year <= toYear)
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());
                suitableGoods.AddRange(context.SheetMusicEditions
                                       .Where(a => a.Description.Contains(researchText)
                                       && a.ReceiptDate != null
                                       && a.ReceiptDate.Value.Year >= fromYear
                                           && a.ReceiptDate.Value.Year <= toYear)
                                       .OrderGoodsBy<Goods>(orderByEnum)
                                       .Page(page, pageSize)
                                       .ToList());
                break;
            default:
                throw new Exception();
        }
        switch (orderByEnum)
        {
            case GoodsOrderBy.Relevance:
#warning how to filter by relevance
                //result.OrderBy(goods => )
                break;
            case GoodsOrderBy.PriceAscending:
                suitableGoods = [.. suitableGoods.OrderBy(goods => goods.Price)];
                break;
            case GoodsOrderBy.PriceDescending:
                suitableGoods = [.. suitableGoods.OrderByDescending(goods => goods.Price)];
                break;
            case GoodsOrderBy.ReceiptDateAscending:
                suitableGoods = [.. suitableGoods.OrderBy(goods => goods.ReceiptDate)];
                break;
            case GoodsOrderBy.ReceiptDateDescending:
                suitableGoods = [.. suitableGoods.OrderByDescending(goods => goods.ReceiptDate)];
                break;
            default:
                throw new Exception();
        }
        var result = new List<KeyValuePair<string, Type>>();
        for (int i = 0; i < 15; i++)
        {
            result.Add(new KeyValuePair<string, Type>(suitableGoods[i].GoodsId.ToString(), suitableGoods[i].GetType()));
        }
        return result;

    }
}
