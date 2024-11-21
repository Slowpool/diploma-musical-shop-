using DataLayer.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;
using DbAccessLayer;

namespace BasicAppConfiguration;

/// <summary>
/// This class aids to solve the DRY problem when i had to configure identity+services+DbContext in both WebApi and MusicalShopApp. I avoid the copy+past stuff in this way, which is considered to be a good manners...
/// </summary>
public static class BasicApp
{
    public static WebApplicationBuilder CreateBuilder(string[] args)
    {
#warning rub it off
        const bool USE_MYSQL = true;
        const bool USE_SQL_SERVER = false;
        const bool USE_SQLITE = false;

        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString(USE_MYSQL ? "MySql" : USE_SQL_SERVER ? "SqlServer" : USE_SQLITE ? "Sqlite" : "") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<MusicalShopDbContext>(options =>
        {
            if (USE_MYSQL)
                options.UseMySql(connectionString, ServerVersion.Parse("8.0.39"));
            else if (USE_SQL_SERVER)
                options.UseSqlServer(connectionString);
            else
                options.UseSqlite(connectionString);
        });
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                        .AddRoles<IdentityRole>()
                        .AddRoleManager<RoleManager<IdentityRole>>()
                        .AddEntityFrameworkStores<MusicalShopDbContext>();

        builder.Services.RegisterServiceLayer();
        builder.Services.RegisterDbAccessLayer();
        return builder;
    }
}
