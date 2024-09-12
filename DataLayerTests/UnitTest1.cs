using DataLayer.Common;

namespace DataLayerTests;

[TestClass]
public class UnitTest1
{
    public MusicalShopContext context;

    [TestInitialize]
    public void SetUp()
    {
        context = new MusicalShopContext();
    }

    [TestMethod]
    public void TestMethod1()
    {
        
    }
}