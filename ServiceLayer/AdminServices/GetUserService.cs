using DataLayer.Common;
using DataLayer.Models;
using DbAccessLayer.AdminPanel;
using Microsoft.EntityFrameworkCore;

namespace ServiceLayer.AdminServices;

public class GetUserService(MusicalShopDbContext context)
{
    private readonly UserDbAccess dbAccess = new(context);
    public async Task<AppUser?> GetUserInfo(Guid userId)
    {
        return await dbAccess.GetUserInfo(userId);
    }
}
