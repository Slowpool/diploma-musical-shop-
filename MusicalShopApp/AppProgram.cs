using DataLayer.Common;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DbAccessLayer;
using ServiceLayer;
using Microsoft.AspNetCore.Mvc.Rendering;
using BasicAppConfiguration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.AdminServices;
using ServiceLayer.GoodsServices;
using NetCore.AutoRegisterDi;
using System.Reflection;
using DbAccessLayer.Admin;
using Microsoft.AspNetCore.Mvc;
using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authorization;

var builder = BasicApp.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.FormInputRenderMode = FormInputRenderMode.AlwaysUseCurrentCulture;
    })
    .AddRazorRuntimeCompilation()
    ;

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IAuthorizationPolicyProvider, RbacPolicy>();

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
    app.UseExceptionHandler("/Goods/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Goods}/{action=Index}/{id?}");

app.MapRazorPages();



app.Run();
