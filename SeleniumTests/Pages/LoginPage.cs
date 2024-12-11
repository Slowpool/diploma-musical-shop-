using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumTests.Pages;
public class LoginPage : WebDriverInkeeper
{
    public LoginPage(IWebDriver webDriver)
    {
        _webDriver = webDriver;
        _webDriver.Navigate().GoToUrl("http://localhost:5042/Identity/Account/Login");
    }

    public async Task<GoodsPage> Login(string email, string password = "Password123!")
    {
        _webDriver.FindElement(By.Id("Input_Email"))
                  .SendKeys(email);

        _webDriver.FindElement(By.Id("Input_Password"))
                  .SendKeys(password);

        _webDriver.FindElement(By.Id("login-submit"))
                  .Submit();

        await Task.Delay(1000);
        return new GoodsPage(_webDriver);
    }
}
