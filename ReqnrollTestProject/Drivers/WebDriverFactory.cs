using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReqnrollSeleniumDemo.Drivers
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateChrome()
        {
            var options = new ChromeOptions();
            
            // Check if running in CI/CD environment
            var isCI = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TF_BUILD")) || // Azure Pipelines
                       !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI")); // Generic CI
            
            if (isCI)
            {
                // Headless mode for CI/CD environments
                options.AddArgument("--headless=new");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--window-size=1920,1080");
            }
            else
            {
                // Normal mode for local development
                options.AddArgument("--start-maximized");
            }

            return new ChromeDriver(options); // Selenium Manager handles versions
        }
    }
}
