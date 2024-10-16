using DataLayer.Common;
using DataLayer.Models;
using DbAccessLayer.AdminPanel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.AdminServices;

public class GetUserService(MusicalShopDbContext context, UserManager<AppUser> userManager)
{
    private readonly UserDbAccess dbAccess = new(context, userManager);
    public async Task<AppUser?> GetUserInfo(Guid userId)
    {
        return await dbAccess.GetUserInfo(userId);
    }
}
