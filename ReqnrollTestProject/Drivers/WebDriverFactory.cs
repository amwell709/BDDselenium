using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReqnrollSeleniumDemo.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateChrome()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            return new ChromeDriver(options); // Selenium Manager handles versions
        }
    }
}
