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

public interface ICreateSaleService : IErrorStorage
{
    Task<Guid?> ArrangeSale(Dictionary<Guid, KindOfGoods> goods, SalePaidBy paidBy);
    Task<Guid?> ReserveSale(Dictionary<Guid, KindOfGoods> goods);
}
public class CreateSaleService(MusicalShopDbContext context, IGetGoodsService service) : ErrorStorage, ICreateSaleService
{
    private readonly RunnerWriteDb<CreateSaleDto, Task<Guid?>> runner = new RunnerWriteDb<CreateSaleDto, Task<Guid?>>(context, new CreateSaleAction(new SalesDbAccess(context)));
    public override IImmutableList<ValidationResult> Errors => runner.Errors;
    public async Task<Guid?> ArrangeSale(Dictionary<Guid, KindOfGoods> goods, SalePaidBy paidBy)
    {
        List<Goods> goodsList = [];
        try
        {
            foreach (var (goodsId, kindOfGoods) in goods)
            {
                goodsList.Add(await service.GetGoodsInfo(goodsId, kindOfGoods));
            }
            return await runner.Run(new CreateSaleDto(goodsList, paidBy));
        }
        catch
        {
#warning DRN violation (Don't Return Null)
            return null;
        }
    }
    public async Task<Guid?> ReserveSale(Dictionary<Guid, KindOfGoods> goods)
    {

    }
}
