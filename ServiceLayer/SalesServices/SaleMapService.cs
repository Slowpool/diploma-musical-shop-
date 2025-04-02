using BizLogicBase.Validation;
using DataLayer.Models;
using ServiceLayer.GoodsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Sales;

namespace ServiceLayer.SalesServices;

public interface ISaleMapService : IErrorAdder
{
    Task<SaleSearchModel> MapIdToSearchModel(Guid saleId);
}

public class SaleMapService(IGetSaleService getSaleService, IGetGoodsUnitsOfSaleService goodsService) : ErrorAdder, ISaleMapService
{
    public async Task<SaleSearchModel> MapIdToSearchModel(Guid saleId)
    {
        var saleView = await getSaleService.GetSaleView(saleId);
        var goodsItemModels = await goodsService.GetGoodsModelsOfSale(saleId);
        return new(saleView.SaleId, saleView.LocalReservationDate, saleView.LocalSaleDate, saleView.LocalReturningDate, saleView.Status, saleView.Total, saleView.PaidBy, goodsItemModels);
    }
}
