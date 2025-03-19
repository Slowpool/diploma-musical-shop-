using BizLogicBase.Common;
using BizLogicBase.Validation;
using DataLayer.Common;
using DataLayer.NotMapped;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Sales.Dto;
using DbAccessLayer;
using BusinessLogicLayer.Sales;

namespace ServiceLayer.SalesServices;

public interface IReservationService : IErrorStorage
{
    Task<Guid?> CreateReservationAsNotComplete(List<Goods> goods, string secretWord);
}

public class CreateReservationService(MusicalShopDbContext context) : ErrorStorage, IReservationService
{
    private readonly RunnerWriteDb<CreateReservationDto, Task<Guid?>> runner = new(context, new CreateReservationAsNotCompleteAction(new SalesDbAccess(context)));
    public Task<Guid?> CreateReservationAsNotComplete(List<Goods> goods, string secretWord) => runner.Run(new CreateReservationDto(goods, secretWord));
}
