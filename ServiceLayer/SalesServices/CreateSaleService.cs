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
    Task<Guid?> CreateSaleAsNotPaid(List<Goods> goods, SalePaidBy? paidBy);
}
/// <summary>
/// This service is separated from SaleManagementService because it uses complicated business logic
/// </summary>
/// <param name="context"></param>
public class CreateSaleService(MusicalShopDbContext context) : ErrorStorage, ICreateSaleService
{
    private readonly RunnerWriteDb<CreateSaleDto, Task<Guid?>> runner = new(context, new CreateSaleAsNotPaidAction(new SalesDbAccess(context)));
    public override IImmutableList<ValidationResult> Errors => runner.Errors;
    // interesting thing about DRN (don't return null), here it is an architectural decision. anyway either HasErrors is true and then returned value won't be used or HasErrors is false, hence the return value is not null
    public async Task<Guid?> CreateSaleAsNotPaid(List<Goods> goodsList, SalePaidBy? paidBy)
        => await runner.Run(new CreateSaleDto(goodsList, paidBy));
}
