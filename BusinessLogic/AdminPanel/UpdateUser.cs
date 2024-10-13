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
using BusinessLogic.AdminPanel.Dto;
using DataLayer.Models;

namespace BusinessLogic.AdminPanel;
public class UpdateUser(UserDbAccess) : ErrorAdder, IBizAction<UpdateUserDto, AppUser?>
{

    public AppUser? Action(UpdateUserDto dto)
    {
        var user = 
    }
}
