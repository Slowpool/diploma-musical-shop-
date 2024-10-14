using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.Common;
using DbAccessLayer.AdminPanel;
using BizLogicBase.Validation;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using BizLogicBase.Common;
using BusinessLogicLayer.AdminPanel.Dto;
using BusinessLogicLayer.AdminPanel;


namespace ServiceLayer.AdminServices;

//public interface IUpdateUserService
//{
//    Task<string?> UpdateUser(UpdateUserDto dto);
//}

public class UpdateUserService : ErrorStorage//, IUpdateUserService
{
    private readonly RunnerWriteDb<UpdateUserDto, Task<string?>> runner;
    public override IImmutableList<ValidationResult> Errors => runner.Errors;
    public override bool HasErrors => runner.HasErrors;
    public UpdateUserService(MusicalShopDbContext context)
    {
        var action = new UpdateUserAction(new(context));
        runner = new RunnerWriteDb<UpdateUserDto, Task<string?>>(context, action);
    }

    public async Task<string?> UpdateUser(UpdateUserDto dto) => await runner.Run(dto);
}
