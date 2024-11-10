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
    Task<SpecificType> GetSpecificType(string specificType);
    Task<List<string>> GetSpecificTypes();
}
public class FindSpecificTypeService(MusicalShopDbContext context) : ISpecificTypeService
{
    public async Task<SpecificType> GetSpecificType(string specificType)
    {
        return await context.SpecificTypes.SingleAsync(type => type.Name.ToLower() == specificType.ToLower());
    }
    public async Task<List<string>> GetSpecificTypes()
    {
        return await context.SpecificTypes.Select(specificType => specificType.Name)
                                          .ToListAsync();
    }
}
