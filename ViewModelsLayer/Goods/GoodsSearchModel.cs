namespace ViewModelsLayer.Goods;

public class GoodsSearchModel
{
    public string ResearchText { get; set; }
    public List<GoodsUnitSearchDto> GoodsUnitModels { get; set; }
    public GoodsOrderByOptions OrderBy { get; set; }
    public GoodsFilterOptions Filter { get; set; }
#warning probably it's redundant thing here because view can work out this value by its own
    public int ResultsCount { get; set; }
}
