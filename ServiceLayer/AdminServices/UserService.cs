using DataLayer.Common;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.AdminServices.Dto;

namespace ServiceLayer.AdminServices;

public class UserService : IUserService
{
    private readonly MusicalShopDbContext context;
    public UserService(MusicalShopDbContext context)
    {
        this.context = context;
    }

    public async Task<AppUser?> GetUserInfo(Guid userId)
    {
        return await context.Users.Where(u => u.Id == userId.ToString())
                                  .SingleOrDefaultAsync();
    }

#warning no business logic
    public async Task<bool> UpdateUser(UpdateUserDto dto)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Id == dto.Id.ToString());
        if (user == null)
            return false;

        user.UserName = dto.UserName;
        user.EmailConfirmed = dto.EmailConfirmed;
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;
        user.PhoneNumberConfirmed = dto.PhoneNumberConfirmed;
        user.TwoFactorEnabled = dto.TwoFactorEnabled;
        user.LockoutEnd = dto.LockoutEnd;
        user.LockoutEnabled = dto.LockoutEnabled;

        context.Update(user);
        await context.SaveChangesAsync();

        return true;
    }
}
