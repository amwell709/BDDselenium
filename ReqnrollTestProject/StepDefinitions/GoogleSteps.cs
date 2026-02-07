using NUnit.Framework;
using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using Reqnroll;
using System.Threading;

namespace ReqnrollSeleniumDemo.StepDefinitions;

[Binding]
public class GoogleSteps
{
    private readonly ScenarioContext _scenarioContext;
    private IWebDriver Driver => _scenarioContext.Get<IWebDriver>("WEB_DRIVER");

    public GoogleSteps(ScenarioContext scenarioContext)
    {
        _scenarioContext = scenarioContext;
    }

    [Given(@"I open the browser")]
    public void GivenIOpenTheBrowser()
    {
        // Browser started in hooks; this step can be a no-op or used for clarity
        Assert.That(Driver, Is.Not.Null);
    }

    [When(@"I navigate to ""(.*)""")]
    public void WhenINavigateTo(string url)
    {
        Driver.Navigate().GoToUrl(url);
        Thread.Sleep(5000);

    }

    [Then(@"the page title should contain ""(.*)""")]
    public void ThenThePageTitleShouldContain(string expected)
    {
        StringAssert.Contains(expected, Driver.Title);
        Console.WriteLine("test is done");
    }
}
