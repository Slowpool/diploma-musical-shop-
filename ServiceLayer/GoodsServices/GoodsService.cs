using Azure;
using BusinessLogicLayer.Goods;
using BusinessLogicLayer.Goods.Dto;
using DataLayer.Common;
using DataLayer.Models.SupportClasses;
using DataLayer.NotMapped;
using DbAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.GoodsServices;

#warning i ain't wanna create service for each action
public class GoodsService(MusicalShopDbContext context)
{
    public async Task<GoodsUnitSearchDto?> GetReadableGoodsInfo(string id, Type type)
    {
        GoodsUnitSearchDto dto = new();
        dto.Id = id;

        Goods goods;
        switch (type.Name)
        {
            case "MusicalInstrument":
                goods = context.MusicalInstruments.SingleOrDefault(e => e.MusicalInstrumentId.ToString() == id)!;

                break;
            case "Accessory":
                goods = context.Accessories.SingleOrDefault(e => e.AccessoryId.ToString() == id)!;

                break;
            case "AudioEquipmentUnit":
                goods = context.AudioEquipmentUnits.SingleOrDefault(e => e.AudioEquipmentUnitId.ToString() == id)!;

                break;
            case "SheetMusicEdition":
                goods = context.SheetMusicEditions.SingleOrDefault(e => e.SheetMusicEditionId.ToString() == id)!;

                break;
            default:
                return null;
        }
        dto.Name = $"{goods.Type} {goods.}";


    }

    public async Task<Dictionary<string, Type>> GetRelevantGoodsIds(string researchText, GoodsFilter filterEnum, GoodsOrderBy orderByEnum, int page, int pageSize)
    {

    }
}
