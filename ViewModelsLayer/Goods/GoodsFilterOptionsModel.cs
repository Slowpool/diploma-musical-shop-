using DataLayer.SupportClasses;

namespace ViewModelsLayer.Goods;

/// <summary>
/// It filters goods, where <paramref name="FilterValue"/> is a value or comma-separated values (depends upon <paramref name="GoodsFilter"/> value).
/// Pass <paramref="FilterValue"/> as something like a string.Join([nameof(type1), nameof(typen)], ',').
/// </summary>
/// <param name="GoodsFilter"></param>
/// <param name="FilterValue"></param>
public record class GoodsFilterOptionsModel(int? MinPrice, int? MaxPrice, DateTimeOffset? FromReceiptDate, DateTimeOffset? ToReceiptDate, KindOfGoods KindOfGoods, GoodsStatus Status);
