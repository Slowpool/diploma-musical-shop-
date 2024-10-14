using BusinessLogic.AdminPanel;
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


namespace ServiceLayer.AdminServices;

public class UpdateUserService : ErrorStorage
{
    private readonly RunnerWriteDb<UpdateUserDto, Task<AppUser?>> runner;
    public override IImmutableList<ValidationResult> Errors => runner.Errors;
    public override bool HasErrors => runner.HasErrors;
    public UpdateUserService(MusicalShopDbContext context)
    {
        var action = new UpdateUserAction(new UserDbAccess(context));
        runner = new RunnerWriteDb<UpdateUserDto, Task<AppUser?>>(context, action);
    }

    public async Task<AppUser?> UpdateUser(UpdateUserDto dto) => await runner.Run(dto);
}
