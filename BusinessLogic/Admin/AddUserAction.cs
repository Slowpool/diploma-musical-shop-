using BizLogicBase.Common;
using BizLogicBase.Validation;
using BusinessLogicLayer.Admin.Dto;
using DataLayer.Models;
using DbAccessLayer.Admin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Admin;
#warning this is a CRUD, so it should be in service layer
public class AddUserAction(UserDbAccess dbAccess) : ErrorAdder, IBizAction<NewUserDto, Task<string?>>
{
    public async Task<string?> Action(NewUserDto dto)
    {
        var newUser = new IdentityUser();

        if (string.IsNullOrWhiteSpace(dto.UserName))
            AddError("Имя пользователя не может быть пустым");
        else
        {
            var normalizedUserName = dto.UserName.ToUpper();
            if (!await dbAccess.IsUniqueNormalizedUserName(normalizedUserName))
                AddError("Данное имя пользователя уже используется", nameof(IdentityUser.UserName));
            else
            {
                newUser.UserName = dto.UserName;
                newUser.NormalizedUserName = normalizedUserName;
            }
        }

        if (string.IsNullOrWhiteSpace(dto.Password))
            AddError("Пароль не может быть пустым");

        newUser.Email = dto.Email;
        newUser.EmailConfirmed = dto.EmailConfirmed;
        newUser.PhoneNumber = dto.PhoneNumber;
        newUser.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
        return await dbAccess.AddUser(newUser, dto.Password);
    }
}
