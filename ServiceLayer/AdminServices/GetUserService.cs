using BusinessLogic.AdminPanel.Dto;
using DataLayer.Common;
using DataLayer.Models;
using DbAccessLayer.AdminPanel;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.AdminServices;

public class GetUserService(UserDbAccess dbAccess) : IGetUserService
{
    public async Task<AppUser?> GetUserInfo(Guid userId)
    {
        return await dbAccess.GetUserInfo(userId);
    }
}
