using DataLayer.Common;
using DataLayer.Models;
using DbAccessLayer.AdminPanel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.AdminServices;

#warning rub it off
const bool USE_MYSQL = true;
const bool USE_SQL_SERVER = false;
const bool USE_SQLITE = false;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString(USE_MYSQL ? "MySql" : USE_SQL_SERVER ? "SqlServer" : USE_SQLITE ? "Sqlite" : "") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<MusicalShopDbContext>(options =>
{
    if (USE_MYSQL)
        options.UseMySql(connectionString, ServerVersion.Parse("8.0.39"));
    else if (USE_SQL_SERVER)
        options.UseSqlServer(connectionString);
    //else
        //options.UseSql();
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<MusicalShopDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<UserDbAccess>();
builder.Services.AddScoped<GetUserService>();
builder.Services.AddScoped<UpdateUserService>();


var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope())
{
    await DataSeeding.SeedAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
