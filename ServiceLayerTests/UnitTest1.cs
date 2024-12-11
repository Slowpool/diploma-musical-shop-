using DataLayer.Common;
using ServiceLayer.StockServices;
using Moq;
using ViewModelsLayer.Stock;
using DataLayer.SupportClasses;
using DataLayer.Models.SpecificTypes;

namespace ServiceLayerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task AddNewValidGoodsItemTest()
        {
            MusicalShopDbContext context = new();
            Mock<ISpecificTypeService> specificTypeService = new();

            string newSpecificType = " онтрабас";
            var kindOfGoods = KindOfGoods.MusicalInstruments;

            specificTypeService.Setup(mock => mock.CreateSpecificType(newSpecificType, kindOfGoods))
                               .ReturnsAsync(new MusicalInstrumentSpecificType { Name = newSpecificType, SpecificTypeId = Guid.Parse("b94a6f24-4ad3-4e71-9ff2-1518a48080e0") });
            specificTypeService.SetupGet(mock => mock.Errors)
                               .Returns([]);
            AddNewGoodsService service = new(context, specificTypeService.Object);

            string name = "Ѕарабас 200";
            AddGoodsToWarehouseDto dto = new(name, null, true, newSpecificType, 1900, GoodsStatus.InStock, "Ётот непревзойденный контрабас сделан из абриперсикокосов", 1, new(kindOfGoods, null, null, null, 2024, ManufacturerType.Master, "John Malone"));
            var goods = await service.AddNewGoods(dto);

            Assert.IsFalse(service.HasErrors);
            Assert.IsTrue(goods.Count() == 1);
            Assert.IsTrue(goods[0].Name == name);
        }
    }
}