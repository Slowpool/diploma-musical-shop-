using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizLogicBase;
using BizLogicBase.Common;
using BizLogicBase.Validation;
using BusinessLogicLayer.AdminPanel.Dto;
using DataLayer.Models;
using DbAccessLayer.AdminPanel;

namespace BusinessLogicLayer.AdminPanel;
public class UpdateUserAction(UserDbAccess dbAccess) : ErrorAdder, IBizAction<UpdateUserDto, Task<string?>>
{
    public async Task<string?> Action(UpdateUserDto dto)
    {
        var user = await dbAccess.GetUserInfo(dto.Id);
        if (user == null)
        {
            AddError("Пользователь с таким идентификатором не найден", nameof(user.Id));
            return null;
        }

        if (string.IsNullOrWhiteSpace(user.UserName))
            AddError("Имя пользователя не может быть пустым", nameof(user.UserName));
        else
        {
            var normalizedUserName = dto.UserName!.ToUpper();
            if (!await dbAccess.IsUniqueNormalizedUserName(normalizedUserName, user.Id))
                AddError("Пользователь с таким именем уже существует", nameof(user.UserName));
            else
            {
                user.UserName = dto.UserName;
                user.NormalizedUserName = normalizedUserName;
            }
        }

        if (dto.EmailConfirmed && string.IsNullOrWhiteSpace(dto.Email))
            AddError("Электронная почта указана как подтврежденная, но ее значение отсутствует", nameof(user.EmailConfirmed), nameof(user.Email));

        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            var normalizedEmail = dto.Email.ToUpper();
            if (!await dbAccess.IsUniqueNormalizedEmail(normalizedEmail, user.Id))
                AddError("Данная электронная почта уже используется", nameof(user.Email));
            else
            {
#warning js to validate
                user.Email = dto.Email;
                user.NormalizedEmail = normalizedEmail;
                user.EmailConfirmed = dto.EmailConfirmed;
            }
        }
        else
        {
            user.Email = null;
            user.NormalizedEmail = null;
            user.EmailConfirmed = false;
        }

        if (dto.PhoneNumber == null)
        {
            if (dto.PhoneNumberConfirmed)
                AddError("Номер телефона указан как подтврежденный, но его значение отсутствует", nameof(user.PhoneNumberConfirmed), nameof(user.PhoneNumber));
        }
        else if(dto.PhoneNumber.Length != 11)
                AddError("Номер телефона указан не полностью", nameof(user.PhoneNumber));

        user.PhoneNumber = dto.PhoneNumber;
        user.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;

        user.TwoFactorEnabled = dto.TwoFactorEnabled;
        user.LockoutEnd = dto.LockoutEnd != null ? ((DateTime)dto.LockoutEnd).ToUniversalTime() : null;
        user.LockoutEnabled = dto.LockoutEnabled;
#error how it's working?
        return user.Id;
    }
}
