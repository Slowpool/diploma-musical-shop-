using DataLayer.Common;
using DataLayer.SupportClasses;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.StockServices;

public interface IFindSpecificTypeService
{
    Task<SpecificType> FindSpecificType(string specificType);
}
public class FindSpecificTypeService(MusicalShopDbContext context) : IFindSpecificTypeService
{
    public async Task<SpecificType> FindSpecificType(string specificType)
    {
        return await context.SpecificTypes.SingleAsync(type => type.Name.ToLower() == specificType.ToLower());
    }
}
