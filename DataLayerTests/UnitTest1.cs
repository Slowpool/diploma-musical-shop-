using DataLayer.Common;

namespace DataLayerTests;

[TestClass]
public class UnitTest1
{
    public MusicalShopDbContext context;

#warning what did i want to test here?
#warning UPD: the same question a month ago. so funny that i asked it again.
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