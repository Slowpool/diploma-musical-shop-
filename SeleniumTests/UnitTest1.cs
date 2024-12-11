using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SeleniumTests.Pages;


namespace SeleniumTests;

[TestClass]
public class UnitTest1 : IDisposable
{
    private IWebDriver _webDriver = new ChromeDriver();

    By LogoutButtonBy = By.Id("logout-button");
    By FirstGoodsItemBy = By.ClassName("mini-card");
    By AddToCartInMiniCardBy = By.XPath("//div[@class='mini-card-right']/form");

    [TestMethod]
    public async Task CartNavBarItemTest()
    {
        var loginPage = new LoginPage(_webDriver);
        var goodsPage = await loginPage.Login("seller@koshka.prosrochka");
        var cartPage = goodsPage.GoToCart();
        
        Assert.IsTrue(cartPage.SummaryDiv.Text.Contains("Корзина пуста."));
    }

    [TestMethod]
    public async Task AddOneGoodsUnitToCartTest()
    {
        var loginPage = new LoginPage(_webDriver);
        await loginPage.Login("seller@koshka.prosrochka");

        //_webDriver.FindElement(FirstGoodsItemBy)
        //          .FindElement(AddToCartInMiniCardBy);

        //_webDriver.FindElement(CartBy)
        //          .Click();

    }

    [TestCleanup]
    public void AfterTest()
    {
        Logout();
    }

    private void Logout()
    {
        _webDriver.FindElement(LogoutButtonBy)
                  .Click();
    }

    public void Dispose()
    {
        _webDriver.Dispose();
    }
}