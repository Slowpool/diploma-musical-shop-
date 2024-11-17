using DataLayer.Common;
using DataLayer.Models.SpecificTypes;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.GoodsServices;
public interface IMapKindOfGoodsService
{
    IQueryable<Goods> MapToSpecificGoods(KindOfGoods kindOfGoods);
    SpecificType CreateNewSpecificType(KindOfGoods kindOfGoods);
    Type MapToType(KindOfGoods kindOfGoods);
    IQueryable<SpecificType> MapToSpecificTypes(KindOfGoods kindOfGoods);

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

    public SpecificType CreateNewSpecificType(KindOfGoods kindOfGoods)
        => (SpecificType)Activator.CreateInstance(MapToType(kindOfGoods))!;

}
