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
using Microsoft.AspNetCore.Identity;


namespace ServiceLayer.AdminServices;

//public interface IUpdateUserService
//{
//    Task<string?> UpdateUser(UpdateUserDto dto);
//}

public class UpdateUserService(MusicalShopDbContext context, UserManager<AppUser> userManager) : ErrorStorage//, IUpdateUserService
{
    private readonly RunnerWriteDb<UpdateUserDto, Task<string?>> _runner = new(context, new UpdateUserAction(new(context, userManager)));
    public override IImmutableList<ValidationResult> Errors => _runner.Errors;

#warning actually here must not be dto
    public async Task<string?> UpdateUser(UpdateUserDto dto) => await _runner.Run(dto);
}
