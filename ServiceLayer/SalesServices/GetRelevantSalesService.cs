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
    Task<List<SaleSearchDto>> GetRelevantSales(string researchText, SalesFilterOptions filterOptions, SalesOrderByOptions orderByOptions);
}
public class GetRelevantSalesService(MusicalShopDbContext context, IGetSaleService getSaleService, IGetGoodsUnitsRelatedToSaleService goodsService) : IGetRelevantSalesService
{
    public async Task<List<SaleSearchDto>> GetRelevantSales(string researchText, SalesFilterOptions filterOptions, SalesOrderByOptions orderByOptions)
    {
        List<Guid> saleIds = [.. context.SalesView
                                            .AsNoTracking()
#warning add researchTextFilter
                                            //.Where()
                                            .FilterBy(filterOptions)
                                            .OrderBy(orderByOptions)
                                            .Select(sale => sale.SaleId)];
        List<SaleSearchDto> result = [];
        SaleView saleView;
        foreach (Guid saleId in saleIds)
        {
            saleView = await getSaleService.GetSaleView(saleId);
            List<Goods> relatedGoods = await goodsService.GetOrigGoodsUnitsRelatedToSale(saleId);
#warning not sure about it
            List<string> briefGoodsDescriptions = [];
            foreach(Goods goodsUnit in relatedGoods)
            {
                briefGoodsDescriptions.Add($"{goodsUnit.Name} {goodsUnit.Description}");
            }
            SaleSearchDto dto = new(saleView.SaleId, saleView.LocalReservationDate, saleView.LocalSaleDate, saleView.LocalReturningDate, saleView.Status, saleView.Total, saleView.PaidBy, briefGoodsDescriptions);
            result.Add(dto);
        }
        return result;
    }
}
