using BizLogicBase.Common;
using BizLogicBase.Validation;
using BusinessLogicLayer.BaseActions;
using BusinessLogicLayer.Misc;
using BusinessLogicLayer.Sales.Dto;
using DataLayer.Common;
using DataLayer.Models;
using DataLayer.NotMapped;
using DataLayer.SupportClasses;
using DbAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Sales;

public class CreateSaleAsNotPaidAction(SalesDbAccess dbAccess) : CreateSaleBaseAction(dbAccess), IBizAction<CreateSaleDto, Task<Guid?>>
{
    /// <summary>
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    public async Task<Guid?> Action(CreateSaleDto dto) => await base.Action(dto.GoodsForSale, TypeOfNewSale.Sale);
}
