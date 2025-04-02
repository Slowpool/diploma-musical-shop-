using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.GoodsServices;
using ServiceLayer.SalesServices.QueryObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Sales;

namespace ServiceLayer.SalesServices;
public interface IGetRelevantSalesService
{
    Task<List<SaleSearchModel>> GetRelevantSales(string researchText, SalesFilterOptions filterOptions, SalesOrderByOptions orderByOptions);
}
public class GetRelevantSalesService(MusicalShopDbContext context, ISaleMapService saleMapperService) : IGetRelevantSalesService
{
    public async Task<List<SaleSearchModel>> GetRelevantSales(string researchText, SalesFilterOptions filterOptions, SalesOrderByOptions orderByOptions)
    {
        List<Guid> saleIds = [.. context.SalesView
                                            .AsNoTracking()
#warning add researchTextFilter
                                            //.Where()
                                            .FilterSalesBy(filterOptions)
                                            .OrderBy(orderByOptions)
                                            .Select(sale => sale.SaleId)];
        List<SaleSearchModel> result = [];
        SaleView saleView;
        foreach (Guid saleId in saleIds)
        {
            result.Add(await saleMapperService.MapIdToSearchModel(saleId));
        }
        return result;
    }
}
