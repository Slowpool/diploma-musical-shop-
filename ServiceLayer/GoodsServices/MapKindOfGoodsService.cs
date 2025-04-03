using DataLayer.Common;
using DataLayer.Models.SpecificTypes;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;

namespace ServiceLayer.GoodsServices;
public interface IMapKindOfGoodsService
{
    IQueryable<Goods> MapToSpecificGoods(KindOfGoods kindOfGoods);
    SpecificType CreateNewSpecificType(KindOfGoods kindOfGoods);
    Type MapToType(KindOfGoods kindOfGoods);
    IQueryable<SpecificType> MapToSpecificTypes(KindOfGoods kindOfGoods);
    Task<KindOfGoods> GetGoodsKind(Guid goodsId);
    string MapToString(KindOfGoods kindOfGoods);
}

public class MapKindOfGoodsService(MusicalShopDbContext context) : IMapKindOfGoodsService
{
    public IQueryable<Goods> MapToSpecificGoods(KindOfGoods kindOfGoods) => kindOfGoods switch
    {
        KindOfGoods.Accessories => context.Accessories,
        KindOfGoods.AudioEquipmentUnits => context.AudioEquipmentUnits,
        KindOfGoods.MusicalInstruments => context.MusicalInstruments,
        KindOfGoods.SheetMusicEditions => context.SheetMusicEditions,
        _ => throw new ArgumentException()
    };

    public IQueryable<SpecificType> MapToSpecificTypes(KindOfGoods kindOfGoods) => kindOfGoods switch
    {
        KindOfGoods.MusicalInstruments => context.MusicalInstrumentSpecificTypes,
        KindOfGoods.Accessories => context.AccessorySpecificTypes,
        KindOfGoods.AudioEquipmentUnits => context.AudioEquipmentUnitSpecificTypes,
        KindOfGoods.SheetMusicEditions => context.SheetMusicEditionSpecificTypes,
        _ => throw new ArgumentException()
    };

    public Type MapToType(KindOfGoods kindOfGoods) => kindOfGoods switch
    {
        KindOfGoods.MusicalInstruments => typeof(MusicalInstrumentSpecificType),
        KindOfGoods.Accessories => typeof(AccessorySpecificType),
        KindOfGoods.AudioEquipmentUnits => typeof(AudioEquipmentUnitSpecificType),
        KindOfGoods.SheetMusicEditions => typeof(SheetMusicEditionSpecificType),
        _ => throw new ArgumentException()
    };

    public string MapToString(KindOfGoods kindOfGoods) => kindOfGoods switch
    {
        KindOfGoods.MusicalInstruments => CommonNames.MusicalInstruments,
        KindOfGoods.Accessories => CommonNames.Accessories,
        KindOfGoods.AudioEquipmentUnits => CommonNames.AudioEquipmentUnits,
        KindOfGoods.SheetMusicEditions => CommonNames.SheetMusicEditions,
        _ => throw new ArgumentException()
    };

    public SpecificType CreateNewSpecificType(KindOfGoods kindOfGoods)
        => (SpecificType)Activator.CreateInstance(MapToType(kindOfGoods))!;

    public async Task<KindOfGoods> GetGoodsKind(Guid goodsId)
    {
        IQueryable<Goods> goods;
        foreach (var kindOfGoods in Enum.GetValues<KindOfGoods>())
        {
            goods = MapToSpecificGoods(kindOfGoods);
            if (goods.Any(g => g.GoodsId == goodsId))
                return kindOfGoods;
        }
        throw new ArgumentException();
    }
}
