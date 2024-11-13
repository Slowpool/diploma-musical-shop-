using BizLogicBase.Common;
using BizLogicBase.Validation;
using BusinessLogicLayer.Sales;
using BusinessLogicLayer.Sales.Dto;
using DataLayer.Common;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using DbAccessLayer;
using ServiceLayer.GoodsServices;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.SalesServices;

public interface IArrangeSaleService : IErrorStorage
{
    Task<Guid?> ArrangeSale(Dictionary<Guid, KindOfGoods> goods, SalePaidBy paidBy);
}
public class ArrangeSaleService(MusicalShopDbContext context, IGetGoodsService service) : ErrorStorage, IArrangeSaleService
{
    private readonly RunnerWriteDb<ArrangeSaleDto, Task<Guid?>> runner = new RunnerWriteDb<ArrangeSaleDto, Task<Guid?>>(context, new ArrangeSaleAction(new SalesDbAccess(context)));
    public override IImmutableList<ValidationResult> Errors => runner.Errors;
    public async Task<Guid?> ArrangeSale(Dictionary<Guid, KindOfGoods> goods, SalePaidBy paidBy)
    {
        List<Goods> goodsList = [];
        foreach(var (goodsId, kindOfGoods) in goods)
        {
            goodsList.Add(await service.GetGoodsInfo(goodsId.ToString(), kindOfGoods));
        }
        return await runner.Run(new ArrangeSaleDto(goodsList, paidBy));
    }
}
