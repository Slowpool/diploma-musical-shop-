using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages;
public class CartPage : WebDriverInkeeper
{
    By CartSummaryBy = By.ClassName("total-cart-price");

    public IWebElement SummaryDiv => _webDriver.FindElements(CartSummaryBy)[0];
    public CartPage(IWebDriver webDriver)
    {
        _webDriver = webDriver;
    }
}
