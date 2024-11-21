using DataLayer.Common;
using DataLayer.Models;
using DbAccessLayer.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.AdminServices;

public interface IGetUserService
{
    Task<IdentityUser?> GetUserInfo(Guid userId);
}

public class GetUserService(MusicalShopDbContext context, UserManager<IdentityUser> userManager) : IGetUserService
{
    private readonly UserDbAccess dbAccess = new(context, userManager);
    public async Task<IdentityUser?> GetUserInfo(Guid userId)
    {
        return await dbAccess.GetUserInfo(userId);
    }
}
