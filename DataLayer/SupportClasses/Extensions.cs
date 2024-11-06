namespace DataLayer.SupportClasses;
public static class Extensions
{
    public static string ToFormattedText(this GoodsStatus goodsStatus) =>
        goodsStatus switch
        {
            GoodsStatus.InStock => "На складе",
            GoodsStatus.AwaitingDelivery => "Ожидает поставки",
            GoodsStatus.Sold => "Продано",
            GoodsStatus.Reserved => "Зарезервировано"
        };

    public static bool TryParse(this string kindOfGoods, out KindOfGoods result) => Enum.TryParse<KindOfGoods>(kindOfGoods, out result);
}
