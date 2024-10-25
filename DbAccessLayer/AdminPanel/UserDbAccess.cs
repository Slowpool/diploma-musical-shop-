using Common;
using DataLayer.Common;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
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
    Task<bool> IsUniqueNormalizedUserName(string normalizedUserName, string userId);
    Task<bool> IsUniqueNormalizedUserName(string normalizedUserName);
    Task<bool> IsUniqueNormalizedEmail(string normalizedEmail, string userId);
    Task<string?> AddUser(AppUser newUser, string password); 
}

public class UserDbAccess(MusicalShopDbContext context, UserManager<AppUser> userManager) : IUserDbAccess
{
#warning don't return null...
    public async Task<AppUser?> GetUserInfo(Guid userId)
    {
        return await context.Users.Where(u => u.Id == userId.ToString())
                                  .SingleOrDefaultAsync();
    }

    public async Task<bool> IsUniqueNormalizedUserName(string normalizedUserName, string userId) =>
        !await context.Users.AnyAsync(u => u.NormalizedUserName == normalizedUserName
            && u.Id != userId);

    public async Task<bool> IsUniqueNormalizedUserName(string normalizedUserName) =>
        await IsUniqueNormalizedUserName(normalizedUserName, CommonNames.NotExistingGuid);

    public async Task<bool> IsUniqueNormalizedEmail(string normalizedEmail, string userId)
    {
        return !await context.Users.AnyAsync(u => u.NormalizedEmail == normalizedEmail && u.Id != userId);
    }

    public async Task<string?> AddUser(AppUser newUser, string password)
    {
        var result = await userManager.CreateAsync(newUser, password);
        if (result.Errors.Any())
            return newUser.Id;
        else
            return null;
    }
}
