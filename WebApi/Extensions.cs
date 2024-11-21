using DataLayer.SupportClasses;

namespace WebApi;

public static class Extensions
{
    public static bool TryParse(this string kindOfGoods)
        => Enum.TryParse(kindOfGoods, out KindOfGoods _);
    //public class KindOfGoodsConstraint<TEnum> : IRouteConstraint
    //        where TEnum : struct, Enum
    //{
    //    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    //    {
    //        var candidate = values[routeKey]?.ToString();
    //        return Enum.TryParse(candidate, true, out TEnum _);
    //    }
    //}
}
