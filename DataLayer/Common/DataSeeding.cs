using Common;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Common;
public class DataSeeding
{
#warning seed another data like types of musical instruments like guitars
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await EnsureRolesAsync(roleManager);

        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        await EnsureUsersAsync(userManager, services);
    }

    public async static Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = { CommonNames.AdminRole, CommonNames.SellerRole, CommonNames.ConsultantRole, CommonNames.StockManagerRole };
        for (int i = 0; i < 4; i++)
        {
            var roleExists = await roleManager.RoleExistsAsync(roles[i]);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole { Name = roles[i] });
            }
        }
    }

    private static async Task EnsureUsersAsync(UserManager<AppUser> userManager, IServiceProvider services)
    {
        var configuration = services.GetRequiredService<IConfiguration>();
        var passwordsSection = configuration.GetSection("DefaultPasswords");
#warning it could have been implemented more, more simply.
        string[] emails = [CommonNames.DefaultAdminEmail, CommonNames.DefaultSellerEmail, CommonNames.DefaultConsultantEmail, CommonNames.DefaultStockManagerEmail];
        string[] roleNames = [CommonNames.AdminRole, CommonNames.SellerRole, CommonNames.ConsultantRole, CommonNames.StockManagerRole];
        for (int i = 0; i < emails.Length; i++)
        {
            var defaultUser = await userManager.Users
                .Where(x => x.UserName == emails[i])
                .SingleOrDefaultAsync();
            if (defaultUser == null)
            {
                var user = new AppUser { UserName = emails[i], Email = emails[i], EmailConfirmed = true };
                await userManager.CreateAsync(user, passwordsSection.GetValue<string>(roleNames[i])!);
                await userManager.AddToRoleAsync(user, roleNames[i]);
            }
        }
        return;
    }
}