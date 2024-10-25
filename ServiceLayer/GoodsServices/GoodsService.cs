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
using DataLayer.SupportClasses;
using System.Net.Http;
using System.Collections;

namespace ServiceLayer.GoodsServices;

#warning i ain't wanna create service for each action
public class GoodsService(MusicalShopDbContext context)
{
#warning i don't like this method. strictly speaking, i see this> (T?)(Goods?) cast for oithe first time
    public async Task<T?> GetGoodsInfo<T>(string id)
        where T : Goods
    {
        IQueryable<Goods>? targetGoods = typeof(T).Name switch
        {
            "MusicalInstrument" => context.MusicalInstruments,
            "Accessory" => context.Accessories,
            "AudioEquipmentUnit" => context.AudioEquipmentUnits,
            "SheetMusicEdition" => context.SheetMusicEditions,
            _ => null,
        };
        return (T?)await targetGoods
            .Include(g => g.Type)
            .SingleOrDefaultAsync(e => e.GoodsId.ToString() == id)!;
    }

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
                dto.Name = $"{specificGoods.Name} от \"{from}\"";
                string description = $"Год выпуска: {specificGoods.ReleaseYear}. ";
                int remainedLength = MAX_LENGTH_OF_BRIEF_GOODS_DESCRIPTION - description.Length;
                int takeDescriptionLength = specificGoods.Description.Length <= remainedLength ? specificGoods.Description.Length : remainedLength;
                description += specificGoods.Description.Substring(0, takeDescriptionLength);
                if (takeDescriptionLength < specificGoods.Description.Length)
                    description += "...";
                dto.Description = description;
                break;
            case "Accessory":
                var accessory = (Accessory)goods;
                string color = accessory.Color.ToLower();
                string size = accessory.Size.ToLower();
                dto.Name = $"{accessory.Name}, {color}, {size}";
                dto.Description = accessory.Description[..MAX_LENGTH_OF_BRIEF_GOODS_DESCRIPTION];
                break;
            case "AudioEquipmentUnit":
                var audioEquipmentUnit = (AudioEquipmentUnit)goods;
                dto.Name = $"{audioEquipmentUnit.Name}";
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
        MethodInfo methodInfo = typeof(GoodsService).GetMethod("GetReadableGoodsInfo", BindingFlags.IgnoreReturn | BindingFlags.Public | BindingFlags.Instance, [typeof(string)])!;
        methodInfo = methodInfo.MakeGenericMethod(type);

        return await (Task<GoodsUnitSearchDto?>)methodInfo.Invoke(this, [id])!;
    }

    // Kinda complex task to implement.
    // Upd: nice. this method is absolutely useless because it is impossible to implement paging for a lightweight quantity of objects in memory. The problem here is that goods of different types are not binded, so it's impossible to know what place item A takes in paging without getting knowledge about others. It may be first and last by match, it's depend upon other items. So, to select little objects in memory won't work. Do anyone understand what did i write here?
    // Consequently, the cause of problem here - i hadn't known what exactly do i implement because i didn't know how the app will look like at all on the whole. I should've sketch out a layout of website = this is the gist.
    public async Task<List<string>> GetRelevantGoodsIds(string researchText, GoodsFilterOptions filterOptions, GoodsOrderByOptions orderByOptions, int page, int pageSize)
    {
        IQueryable<Goods> goods;
#warning separate it in few methods
        switch (filterOptions.KindOfGoods)
        {
            case KindOfGoods.Accessories:
                goods = context.Accessories;
                break;
            case KindOfGoods.AudioEquipmentUnits:
                goods = context.AudioEquipmentUnits;
                break;
            case KindOfGoods.MusicalInstruments:
                goods = context.MusicalInstruments;
                break;
            case KindOfGoods.SheetMusicEditions:
                goods = context.SheetMusicEditions;
                break;
            default:
                throw new Exception();
        }
        goods = goods.AsNoTracking();
        goods = goods.Include(g => g.Type);
        goods = goods.Where(g => g.Status == filterOptions.Status);
        goods = goods.Where(g => g.Description.Contains(researchText) || g.Name.Contains(researchText) || g.Type.Name.Contains(researchText));
        if (filterOptions.MinPrice != null)
            goods = goods.Where(g => g.Price >= filterOptions.MinPrice);
        if (filterOptions.MaxPrice != null)
            goods = goods.Where(g => g.Price <= filterOptions.MaxPrice);
        if (filterOptions.FromReceiptDate != null)
            goods = goods.Where(g => g.ReceiptDate >= filterOptions.FromReceiptDate);
        if (filterOptions.ToReceiptDate != null)
            goods = goods.Where(g => g.ReceiptDate <= filterOptions.ToReceiptDate);

        switch (orderByOptions.OrderBy)
        {
            case GoodsOrderBy.Relevance:
                goods = orderByOptions.AscendingOrder
                    ? goods.OrderBy(g => g.Type.Name)
                    : goods.OrderByDescending(g => g.Type.Name);
                break;
            case GoodsOrderBy.Price:
                goods = orderByOptions.AscendingOrder
                    ? goods.OrderBy(g => g.Price)
                    : goods.OrderByDescending(g => g.Price);
                break;
            case GoodsOrderBy.ReceiptDate:
                goods = orderByOptions.AscendingOrder
                    ? goods.OrderBy(g => g.ReceiptDate)
                    : goods.OrderByDescending(g => g.ReceiptDate);
                break;
            default:
                throw new Exception();
        }
        goods = goods.Page(page, pageSize);
        List<string> result = [];
        foreach (var goodsUnit in goods)
        {
            result.Add(goodsUnit.GoodsId.ToString());
        }
        return result;

        #region buried previous version
        //        var suitableGoods = new List<Goods>();
        //        switch (filterOptions.GoodsFilter)
        //        {
        //            // I added paging for each table here to reduce in-memory size of data.
        //            // Then I page them again. At first, there's collection of 15 + 15 + 15 + 15 = 60 rows,
        //            // than 15 rows paging happens again.
        //#warning it can be implemented via generic avoiding copy-past
        //            case GoodsFilter.None:
        //                suitableGoods.AddRange(context.Accessories
        //                                       .Where(a => a.Description.Contains(researchText))
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //#warning i screw up with paging.
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                suitableGoods.AddRange(context.MusicalInstruments
        //                                       .Where(a => a.Description.Contains(researchText))
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                suitableGoods.AddRange(context.AudioEquipmentUnits
        //                                       .Where(a => a.Description.Contains(researchText))
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                suitableGoods.AddRange(context.SheetMusicEditions
        //                                       .Where(a => a.Description.Contains(researchText))
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());

        //                break;
        //            case GoodsFilter.Type:
        //                string[] types = filterOptions.FilterValue.Split(',');
        //                if (types.Length == 0)
        //                    throw new ArgumentException("goods filter value was empty");
        //                foreach (string type in types)
        //                {
        //                    switch (type.ToLower())
        //                    {
        //                        case "accessories":
        //                            suitableGoods.AddRange(context.Accessories
        //                                       .Where(a => a.Description.Contains(researchText))
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                            break;
        //                        case "musicalinstruments":
        //                            suitableGoods.AddRange(context.MusicalInstruments
        //                                                   .Where(a => a.Description.Contains(researchText))
        //                                                   .OrderGoodsBy<Goods>(orderByEnum)
        //                                                   .Page(page, pageSize)
        //                                                   .ToList());
        //                            break;
        //                        case "audioequipmentunits":
        //                            suitableGoods.AddRange(context.AudioEquipmentUnits
        //                                                   .Where(a => a.Description.Contains(researchText))
        //                                                   .OrderGoodsBy<Goods>(orderByEnum)
        //                                                   .Page(page, pageSize)
        //                                                   .ToList());
        //                            break;
        //                        case "sheetmusiceditions":
        //                            suitableGoods.AddRange(context.SheetMusicEditions
        //                                                   .Where(a => a.Description.Contains(researchText))
        //                                                   .OrderGoodsBy<Goods>(orderByEnum)
        //                                                   .Page(page, pageSize)
        //                                                   .ToList());
        //                            break;
        //                        default:
        //                            throw new ArgumentException("unknown goods filter value");
        //                    }
        //                }
        //                break;
        //            case GoodsFilter.ReceiptDate:
        //                string[] dates = filterOptions.FilterValue.Split(',');
        //                DateTime fromDate = DateTime.Parse(dates[0]);
        //                DateTime toDate = DateTime.Parse(dates[1]);
        //                suitableGoods.AddRange(context.Accessories
        //                                       .Where(a => a.Description.Contains(researchText)
        //                                           && a.ReceiptDate >= fromDate
        //                                           && a.ReceiptDate <= toDate)
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                suitableGoods.AddRange(context.MusicalInstruments
        //                                       .Where(a => a.Description.Contains(researchText)
        //                                           && a.ReceiptDate >= fromDate
        //                                           && a.ReceiptDate <= toDate)
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                suitableGoods.AddRange(context.AudioEquipmentUnits
        //                                       .Where(a => a.Description.Contains(researchText)
        //                                           && a.ReceiptDate >= fromDate
        //                                           && a.ReceiptDate <= toDate)
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                suitableGoods.AddRange(context.SheetMusicEditions
        //                                       .Where(a => a.Description.Contains(researchText)
        //                                           && a.ReceiptDate >= fromDate
        //                                           && a.ReceiptDate <= toDate)
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                break;
        //            case GoodsFilter.ReleaseYear:
        //                string[] years = filterOptions.FilterValue.Split(',');
        //                int fromYear = int.Parse(years[0]);
        //                int toYear = int.Parse(years[0]);
        //                suitableGoods.AddRange(context.Accessories
        //                                       .Where(a => a.Description.Contains(researchText)
        //                                       && a.ReceiptDate != null
        //                                       && a.ReceiptDate.Value.Year >= fromYear
        //                                           && a.ReceiptDate.Value.Year <= toYear)
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                suitableGoods.AddRange(context.MusicalInstruments
        //                                       .Where(a => a.Description.Contains(researchText)
        //                                       && a.ReceiptDate != null
        //                                       && a.ReceiptDate.Value.Year >= fromYear
        //                                           && a.ReceiptDate.Value.Year <= toYear)
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                suitableGoods.AddRange(context.AudioEquipmentUnits
        //                                       .Where(a => a.Description.Contains(researchText)
        //                                       && a.ReceiptDate != null
        //                                       && a.ReceiptDate.Value.Year >= fromYear
        //                                           && a.ReceiptDate.Value.Year <= toYear)
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                suitableGoods.AddRange(context.SheetMusicEditions
        //                                       .Where(a => a.Description.Contains(researchText)
        //                                       && a.ReceiptDate != null
        //                                       && a.ReceiptDate.Value.Year >= fromYear
        //                                           && a.ReceiptDate.Value.Year <= toYear)
        //                                       .OrderGoodsBy<Goods>(orderByEnum)
        //                                       .Page(page, pageSize)
        //                                       .ToList());
        //                break;
        //            default:
        //                throw new Exception();
        //        }
        //        switch (orderByEnum)
        //        {
        //            case GoodsOrderBy.Relevance:
        //#warning how to filter by relevance
        //                //result.OrderBy(goods => )
        //                break;
        //            case GoodsOrderBy.PriceAscending:
        //                suitableGoods = [.. suitableGoods.OrderBy(goods => goods.Price)];
        //                break;
        //            case GoodsOrderBy.PriceDescending:
        //                suitableGoods = [.. suitableGoods.OrderByDescending(goods => goods.Price)];
        //                break;
        //            case GoodsOrderBy.ReceiptDateAscending:
        //                suitableGoods = [.. suitableGoods.OrderBy(goods => goods.ReceiptDate)];
        //                break;
        //            case GoodsOrderBy.ReceiptDateDescending:
        //                suitableGoods = [.. suitableGoods.OrderByDescending(goods => goods.ReceiptDate)];
        //                break;
        //            default:
        //                throw new Exception();
        //        }
        //        var result = new List<KeyValuePair<string, Type>>();
        //        for (int i = 0; i < 15; i++)
        //        {
        //            result.Add(new KeyValuePair<string, Type>(suitableGoods[i].GoodsId.ToString(), suitableGoods[i].GetType()));
        //        }
        //        return result; 
        #endregion
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

    // neat stuff
    public async Task<string?> AddToOrRemoveFromCart(string goodsId, bool isInCart, string? goodsIdsAndTypes)
    {
        List<string> goodsIdsTypesList = goodsIdsAndTypes?.Split(CommonNames.GoodsIdSeparator, StringSplitOptions.RemoveEmptyEntries)
                                                         ?.ToList() ?? [];
        List<string>? updatedGoodsIdsList = isInCart ? RemoveFromCart(goodsId, goodsIdsTypesList) : await AddInCart(goodsId, goodsIdsTypesList);
        if (updatedGoodsIdsList == null)
            return null;
        else
            return string.Join(CommonNames.GoodsIdSeparator, updatedGoodsIdsList);
    }

    private List<string>? RemoveFromCart(string goodsId, List<string> goodsIdsTypesList)
    {
        foreach (var goodsIdType in goodsIdsTypesList)
        {
            if (goodsIdType.Contains(goodsId))
            {
                goodsIdsTypesList.Remove(goodsIdType);
                return goodsIdsTypesList;
            }
        }
        return null;
    }

    private async Task<List<string>?> AddInCart(string goodsId, List<string> goodsIdsTypesList)
    {
        foreach (var idType in goodsIdsTypesList)
        {
            if (idType.Contains(goodsId))
                return null;
        }
        string typeName = (await GetGoodsType(goodsId)).Name;
        goodsIdsTypesList.Add($"{goodsId}{CommonNames.GoodsIdTypeSeparator}{typeName}");
        return goodsIdsTypesList;
    }
}
