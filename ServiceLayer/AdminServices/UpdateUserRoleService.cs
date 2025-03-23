using BizLogicBase.Validation;
using DataLayer.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelsLayer.Admin;

namespace ServiceLayer.AdminServices;

public interface IUpdateUserRoleService : IErrorAdder
{
    Task UpdateRole(UpdateUserRoleDto dto);

}

public class UpdateUserRoleService(MusicalShopDbContext context, UserManager<IdentityUser> userManager): ErrorAdder, IUpdateUserRoleService
{
    public async Task UpdateRole(UpdateUserRoleDto dto)
    {
        var user = await context.Users.SingleAsync(u => u.Id == dto.UserId.ToString());
        if (user is null)
        {
            AddError("Such a user not found");
            return;
        }

        if (dto.Add) 
            await userManager.AddToRoleAsync(user, dto.RoleName);
        else
            await userManager.RemoveFromRoleAsync(user, dto.RoleName);

    }

}
