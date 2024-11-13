using DataLayer.Common;

namespace DataLayerTests;

[TestClass]
public class UnitTest1
{
    public MusicalShopDbContext context;

#warning what did i want to test here?
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