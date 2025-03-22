namespace ViewModelsLayer.Goods;

public class GoodsSearchModel
{
    public string ResearchText { get; set; }
    public List<GoodsUnitSearchModel> GoodsUnitModels { get; set; }
    public GoodsOrderByOptions OrderBy { get; set; }
    public GoodsFilterOptionsModel Filter { get; set; }
#warning probably it's redundant thing here because view can work out this value by its own
    public int ResultsCount { get; set; }
}
