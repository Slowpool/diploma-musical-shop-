using BizLogicBase.Common;
using BizLogicBase.Validation;
using BusinessLogicLayer.AdminPanel.Dto;
using DataLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.AdminServices
{
    class AddUserService(MusicalShopDbContext context) : ErrorStorage
    {
        private readonly AddUserAction
        private readonly RunnerWriteDb<NewUserDto, Task<string?>> _runner = new RunnerWriteDb<NewUserDto, Task<string?>>(context, )
    }
}
