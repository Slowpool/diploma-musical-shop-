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
public class UpdateUserAction(IUserDbAccess dbAccess) : ErrorAdder, IBizAction<UpdateUserDto, Task<AppUser?>>
{

    public async Task<AppUser?> Action(UpdateUserDto dto)
    {
        var user = await dbAccess.GetUserInfo(dto.Id);
        if (user == null)
        {
            Errors.Add(new ValidationResult("Пользователь с таким идентификатором не найден", [nameof(user.Id)]));
            return null;
        }

        if (string.IsNullOrEmpty(user.UserName))
        {
            Errors.Add(new ValidationResult("Имя пользователя не может быть пустым", [nameof(user.UserName)]));
            return null;
        }
        var normalizedUserName = dto.UserName!.ToUpper();
        if (!await dbAccess.IsSingleNormalizedUserName(normalizedUserName))
        {
            Errors.Add(new ValidationResult("Пользователь с таким именем уже существует", [nameof(user.UserName)]));
            return null;
        }
        user.UserName = dto.UserName;
        user.NormalizedUserName = normalizedUserName;

        if (dto.EmailConfirmed && string.IsNullOrEmpty(user.Email))
        {
            Errors.Add(new ValidationResult("Электронная почта указана как подтврежденная, но ее значение отсутствует", [nameof(user.EmailConfirmed), nameof(user.Email)]));
            return null;
        }

        if (!string.IsNullOrEmpty(user.Email))
        {
            var normalizedEmail = dto.Email.ToUpper();
            if (!await dbAccess.IsSingleNormalizedEmail(normalizedEmail))
            {
                Errors.Add(new ValidationResult("Данная электронная почта уже используется", [nameof(user.Email)]));
                return null;
            }
            user.NormalizedEmail = normalizedEmail;
        }

        
        user.EmailConfirmed = dto.EmailConfirmed;
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;
        user.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
        user.TwoFactorEnabled = dto.TwoFactorEnabled;
        user.LockoutEnd = dto.LockoutEnd != null ? ((DateTime)dto.LockoutEnd).ToUniversalTime() : null;
        user.LockoutEnabled = dto.LockoutEnabled;

        context.Update(user);
        await context.SaveChangesAsync();

    }

}
