using ConstNames;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Common;
public class SeedingData
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await EnsureRolesAsync(roleManager);

        var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
        await EnsureTestAdminAsync(userManager);
    }

    public async static Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var adminRoleExists = await roleManager.RoleExistsAsync(CommonNames.AdminRole);
        if (!adminRoleExists)
        {
            await roleManager.CreateAsync(new IdentityRole { Name = CommonNames.AdminRole });
        }
    }

    private static async Task EnsureTestAdminAsync(UserManager<IdentityUser> userManager)
    {
        var user = userManager.FindByNameAsync(CommonNames.AdminRoleProfile);
        if ()

    }

}