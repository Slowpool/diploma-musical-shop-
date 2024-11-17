using BizLogicBase.Validation;
using DataLayer.Common;
using DataLayer.SupportClasses;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.GoodsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.StockServices;

public interface ISpecificTypeService : IErrorAdder
{
    Task<SpecificType> GetSpecificType(string specificType, KindOfGoods kindOfGoods);
    // TODO probably merge in one method because the first one is just specificTypes.Select(...)
    Task<List<string>> ListTheSpecificTypes(KindOfGoods kindOfGoods);

    Task<Dictionary<KindOfGoods, List<string>>> GetAllSpecificTypes();
    Task<SpecificType> CreateSpecificType(string? newSpecificType, KindOfGoods kindOfGoods);
}
public class SpecificTypeService(MusicalShopDbContext context, IMapKindOfGoodsService kindOfGoodsMapper) : ErrorAdder, ISpecificTypeService
{
#error handle errors to display them
    public async Task<SpecificType> GetSpecificType(string specificType, KindOfGoods kindOfGoods)
        => kindOfGoodsMapper.MapToSpecificTypes(kindOfGoods).Single(st => st.Name.ToLower() == specificType.ToLower());

    public async Task<List<string>> ListTheSpecificTypes(KindOfGoods kindOfGoods)
        => await kindOfGoodsMapper.MapToSpecificTypes(kindOfGoods)
                                  .Select(st => st.Name)
                                  .ToListAsync();

    public async Task<Dictionary<KindOfGoods, List<string>>> GetAllSpecificTypes()
    {
        Dictionary<KindOfGoods, List<string>> specificTypes = new();
        foreach (var kindOfGoods in Enum.GetValues<KindOfGoods>())
            specificTypes[kindOfGoods] = await ListTheSpecificTypes(kindOfGoods);
        return specificTypes;
    }

    // TODO maybe create or update? 
    public async Task<SpecificType> CreateSpecificType(string newSpecificType, KindOfGoods kindOfGoods)
    {
        var correspondingSpecificTypes = kindOfGoodsMapper.MapToSpecificTypes(kindOfGoods);
        // TODO should i listen to ide's advice and use another comparison here?
        if (await correspondingSpecificTypes
                     .AnyAsync(specificType => specificType.Name.ToLower() == newSpecificType.ToLower()))
        {
            AddError($"Тип \"{newSpecificType}\" уже существует");
            return null;
        }
        SpecificType newSpecificTypeEntity = kindOfGoodsMapper.CreateNewSpecificType(kindOfGoods);
        newSpecificTypeEntity.Name = newSpecificType;
        context.Add(newSpecificTypeEntity);
        context.SaveChanges();
        return newSpecificTypeEntity;
    }
}