using DataLayer.SupportClasses;

namespace ViewModelsLayer.Goods;

public class GoodsUnitSearchModel
{
    public Guid Id { get; set; }
	public KindOfGoods KindOfGoods { get; set; }
	public string SpecificType { get; set; }
	public string Name { get; set; }
	public string? Description { get; set; }
	public int Price { get; set; }
	public GoodsStatus Status { get; set; }
	public bool IsInCart { get; set; }
}
