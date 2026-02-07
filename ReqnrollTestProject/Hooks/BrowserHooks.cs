using OpenQA.Selenium;
using Reqnroll;
using ReqnrollSeleniumDemo.Drivers;

namespace ReqnrollSeleniumDemo.Hooks;

[Binding]
public class BrowserHooks
{
    private readonly ScenarioContext _scenarioContext;

    public BrowserHooks(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        IWebDriver driver = WebDriverFactory.CreateChrome();
        _scenarioContext.Set(driver, "WEB_DRIVER");
    }

    [AfterScenario]
    public void AfterScenario()
    {
        if (_scenarioContext.TryGetValue("WEB_DRIVER", out IWebDriver driver))
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}
