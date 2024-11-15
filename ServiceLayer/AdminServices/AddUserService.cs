using BizLogicBase.Common;
using BizLogicBase.Validation;
using BusinessLogicLayer.Admin.Dto;
using BusinessLogicLayer.Admin;
using DataLayer.Common;
using DataLayer.Models;
using DbAccessLayer.Admin;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AdminServices
{
    class AddUserService(MusicalShopDbContext context, UserManager<AppUser> userManager) : ErrorStorage
    {
        private readonly RunnerWriteDb<NewUserDto, Task<string?>> _runner = new(context, new AddUserAction(new(context, userManager)));
        public override IImmutableList<ValidationResult> Errors => _runner.Errors;

        public async Task<string?> Add(string? userName, string? email, bool emailConfirmed, string? phoneNumber, bool phoneNumberConfirmed, string password)
            => await _runner.Run(new NewUserDto(userName, email, emailConfirmed, phoneNumber, phoneNumberConfirmed, password));
    }
}
