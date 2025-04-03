using BizLogicBase.Validation;
using DataLayer.SupportClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.GoodsServices;
public interface ISpecificTypesParserService : IErrorAdder
{
    Task<Dictionary<KindOfGoods, Dictionary<Guid, string>>> Parse();
}

public class SpecificTypesParserService(IMapKindOfGoodsService kindOfgoodsService) : ErrorAdder, ISpecificTypesParserService
{
    public async Task<Dictionary<KindOfGoods, Dictionary<Guid, string>>> Parse()
    {
        Dictionary<KindOfGoods, Dictionary<Guid, string>> specificTypes = [];
        foreach (var kindOfGoods in Enum.GetValues<KindOfGoods>())
        {
            specificTypes[kindOfGoods] = await kindOfgoodsService.MapToSpecificTypes(kindOfGoods)
                .Select(st => new { st.SpecificTypeId, st.Name })
                .ToDictionaryAsync(st => st.SpecificTypeId, st => st.Name);
        }
        return specificTypes;
    }
}