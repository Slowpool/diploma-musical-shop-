using DataLayer.Common;

namespace DataLayerTests;

[TestClass]
public class UnitTest1
{
    public MusicalShopDbContext context;

    [TestInitialize]
    public void SetUp()
    {
        context = new MusicalShopDbContext();
    }

    [TestMethod]
    public void TestMethod1()
    {
        
    }
}