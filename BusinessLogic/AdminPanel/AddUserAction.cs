using BizLogicBase.Common;
using BizLogicBase.Validation;
using BusinessLogicLayer.AdminPanel.Dto;
using DataLayer.Models;
using DbAccessLayer.AdminPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.AdminPanel;
public class AddUserAction(UserDbAccess dbAccess) : ErrorAdder, IBizAction<NewUserDto, Task<string?>>
{
    public async Task<string?> Action(NewUserDto dto)
    {
        var newUser = new AppUser();

        if (string.IsNullOrWhiteSpace(dto.UserName))
        {
            var normalizedUserName = dto.UserName.ToUpper();
            if (!await dbAccess.IsUniqueNormalizedUserName(normalizedUserName))
#warning why do i add property of error if i don't use it?
                AddError("Данное имя пользователя уже используется", nameof(AppUser.UserName));
            else
                newUser.UserName = dto.UserName;
        }
        else
            AddError("Имя пользователя не может быть пустым");

        if (string.IsNullOrWhiteSpace(dto.Password))
            AddError("Пароль не может быть пустым");

        newUser.Email = dto.Email;
        newUser.EmailConfirmed = dto.EmailConfirmed;
        newUser.PhoneNumber = dto.PhoneNumber;
        newUser.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
        return await dbAccess.AddUser(newUser, dto.Password);
    }
}
