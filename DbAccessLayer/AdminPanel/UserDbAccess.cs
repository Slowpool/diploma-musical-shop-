using DataLayer.Common;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLayer.AdminPanel;
public interface IUserDbAccess
{
    Task<AppUser?> GetUserInfo(Guid userId);
    Task<bool> IsSingleNormalizedEmail(string normalizedEmail);
    Task<bool> IsSingleNormalizedUserName(string userName);
}

public class UserDbAccess(MusicalShopDbContext context) : IUserDbAccess
{
    public async Task<AppUser?> GetUserInfo(Guid userId)
    {
        return await context.Users.Where(u => u.Id == userId.ToString())
                                  .SingleOrDefaultAsync();
    }

    public async Task<bool> IsSingleNormalizedUserName(string normalizedUserName)
    {
        return await context.Users.AnyAsync(u => u.NormalizedUserName == normalizedUserName);
    }

    public async Task<bool> IsSingleNormalizedEmail(string normalizedEmail)
    {
        return await context.Users.AnyAsync(u => u.NormalizedEmail == normalizedEmail);
    }
}
