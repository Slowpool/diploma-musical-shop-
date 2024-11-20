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
    Task<Guid?> ArrangeSale(List<Goods> goods, SalePaidBy? paidBy);
}
public class CreateSaleService(MusicalShopDbContext context) : ErrorStorage, ICreateSaleService
{
    private readonly RunnerWriteDb<CreateSaleDto, Task<Guid?>> runner = new RunnerWriteDb<CreateSaleDto, Task<Guid?>>(context, new CreateSaleAction(new SalesDbAccess(context)));
    public override IImmutableList<ValidationResult> Errors => runner.Errors;
    // interesting thing about DRN, here it is an architectural decision.
    public async Task<Guid?> ArrangeSale(List<Goods> goodsList, SalePaidBy? paidBy)
        => await runner.Run(new CreateSaleDto(goodsList, paidBy));
}
