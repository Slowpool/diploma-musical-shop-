using DataLayer.Common;
using DataLayer.SupportClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.StockServices;

public interface ISpecificTypeService
{
    Task<SpecificType> GetSpecificType(string specificType, KindOfGoods kindOfGoods);
    Task<List<string>> GetSpecificTypes(KindOfGoods kindOfGoods);
    Task<Dictionary<KindOfGoods, List<string>>> GetAllSpecificTypes();
}
public class SpecificTypeService(MusicalShopDbContext context) : ISpecificTypeService
{
    public async Task<SpecificType> GetSpecificType(string specificType, KindOfGoods kindOfGoods)
    {

        throw new NotImplementedException();
        // TODO specific type
        //return await context.SpecificTypes.SingleAsync(type => type.Name.ToLower() == specificType.ToLower());
    }

    public async Task<List<string>> GetSpecificTypes(KindOfGoods kindOfGoods) => kindOfGoods switch
    {
        // TODO implement it better
        KindOfGoods.MusicalInstruments => await context.MusicalInstrumentSpecificTypes.Select(st => st.Name).ToListAsync(),
        KindOfGoods.Accessories => await context.AccessorySpecificTypes.Select(st => st.Name).ToListAsync(),
        KindOfGoods.AudioEquipmentUnits => await context.AudioEquipmentUnitSpecificTypes.Select(st => st.Name).ToListAsync(),
        KindOfGoods.SheetMusicEditions => await context.SheetMusicEditionSpecificTypes.Select(st => st.Name).ToListAsync(),
        _ => throw new ArgumentException()
    };
    public async Task<Dictionary<KindOfGoods, List<string>>> GetAllSpecificTypes()
    {
        Dictionary<KindOfGoods, List<string>> specificTypes = new();
        foreach(var kindOfGoods in Enum.GetValues<KindOfGoods>())
            specificTypes[kindOfGoods] = await GetSpecificTypes(kindOfGoods);
        return specificTypes;
    }
    
}
