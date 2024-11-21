using WebApi.RouteGroups;
using DbAccessLayer;
using ServiceLayer;
using DataLayer.Common;
using DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using BasicAppConfiguration;

var builder = BasicApp.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/api")
   .MapApi();

app.Run();
