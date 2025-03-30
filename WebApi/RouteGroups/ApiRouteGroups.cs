using DataLayer.SupportClasses;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.GoodsServices;
using WebApi.Dto;

namespace WebApi.RouteGroups;

public static class ApiRouteGroups
{
    // It forces server to use internet-connection, which is not written in the tt
    //    app.MapGet("/api/v1/goods/{kindOfGoods:KindOfGoods}/{goodsId:Guid}",
    //    ([FromRoute] Guid goodsId, [FromRoute] KindOfGoods kindOfGoods, [FromServices] IGetGoodsService service) =>
    //    {
    //        var goods = service.GetGoodsInfo(goodsId, kindOfGoods);
    //        if (service.)
    //});
    //app.MapPost("/api/reserve");
    public static RouteGroupBuilder MapApi(this RouteGroupBuilder group)
    {
        group.MapGroup("/v1")
             .RouteVersion1();

        return group;
    }

    private static RouteGroupBuilder RouteVersion1(this RouteGroupBuilder group)
    {
        group.MapGet("/goods/{kindOfGoods}/{goodsId:Guid}",
        ([FromRoute] Guid goodsId, [FromRoute] KindOfGoods kindOfGoods, [FromServices] IGetGoodsService service) =>
        {
            try
            {
                var goods = service.GetOrigGoodsItem(goodsId, kindOfGoods);
                return Results.Json(goods);
            }
            catch
            {
                return Results.NotFound();
            }
        });

        group.MapPost("/goods/reserve", ([FromBody] List<GoodsUnitForReservation> goods) =>
        {

        });
        return group;
    }
}
