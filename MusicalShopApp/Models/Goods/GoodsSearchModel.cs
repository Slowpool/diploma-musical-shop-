using BusinessLogicLayer.Goods.Dto;
using DataLayer.Models.SupportClasses;

namespace MusicalShopApp.Models.Goods;

public class GoodsSearchModel
{
	public string ResearchText { get; set; }
	public List<GoodsUnitSearchDto> GoodsUnitModels { get; set; }
	public GoodsOrderBy OrderBy { get; set; }
	public GoodsFilter Filter { get; set; }

}
