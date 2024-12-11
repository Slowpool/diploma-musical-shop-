using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages;
public class GoodsPage : WebDriverInkeeper
{
    By CartBy = By.Id("Goods-Cart");

    public GoodsPage(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }

    public CartPage GoToCart()
    {
        _webDriver.FindElement(CartBy)
                  .Click();
        return new CartPage(_webDriver);
    }
}
