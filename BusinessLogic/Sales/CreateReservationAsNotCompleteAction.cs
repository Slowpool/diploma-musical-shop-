using BizLogicBase.Common;
using BizLogicBase.Validation;
using BusinessLogicLayer.BaseActions;
using BusinessLogicLayer.Misc;
using BusinessLogicLayer.Sales.Dto;
using DbAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Sales;

public class CreateReservationAsNotCompleteAction(SalesDbAccess dbAccess) : CreateSaleBaseAction(dbAccess), IBizAction<CreateReservationDto, Task<Guid?>>
{
    public async Task<Guid?> Action(CreateReservationDto dto) => await base.Action(dto.GoodsForReservation, TypeOfNewSale.Reservation);
}
