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
        if (dto.UserName != null)
        {
            var normalizedUserName = dto.UserName.ToUpper();
            if (await dbAccess.IsUniqueNormalizedUserName(normalizedUserName))
            {
#warning why do i add property of error if i don't use it?
                AddError("Данное имя пользователя уже используется", nameof(AppUser.UserName));
            }
        }
    }
}
