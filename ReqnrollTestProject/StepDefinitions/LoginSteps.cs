using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using Reqnroll;
using ReqnrollSeleniumDemo.Pages;

namespace ReqnrollSeleniumDemo.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver Driver => _scenarioContext.Get<IWebDriver>("WEB_DRIVER");
        private LoginPage? _loginPage;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I navigate to the login page")]
        public void GivenINavigateToTheLoginPage()
        {
            _loginPage = new LoginPage(Driver);
            _loginPage.NavigateToLoginPage();
            Thread.Sleep(2000); // Wait for page to load
        }

        [When(@"I enter email ""(.*)""")]
        public void WhenIEnterEmail(string email)
        {
            _loginPage.EnterEmail(email);
        }

        [When(@"I enter password ""(.*)""")]
        public void WhenIEnterPassword(string password)
        {
            _loginPage.EnterPassword(password);
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _loginPage.ClickLoginButton();
            Thread.Sleep(3000); // Wait for login to complete
        }

        [Then(@"I should be logged in successfully")]
        public void ThenIShouldBeLoggedInSuccessfully()
        {
            // Check if login was successful
            if (_loginPage.IsErrorMessageDisplayed())
            {
                string errorMsg = _loginPage.GetErrorMessage();
                Console.WriteLine($"Login failed with error: {errorMsg}");
                Console.WriteLine($"Current URL: {_loginPage.GetCurrentUrl()}");
                Assert.Fail($"Login failed. Error message displayed: {errorMsg}");
            }
            
            // Verify successful login
            Assert.That(_loginPage.IsLoginSuccessful(), Is.True, "Login should be successful");
        }

        [Then(@"I should see the My Account page")]
        public void ThenIShouldSeeTheMyAccountPage()
        {
            Console.WriteLine($"Current URL: {_loginPage.GetCurrentUrl()}");
            Assert.That(_loginPage.IsMyAccountHeaderDisplayed(), Is.True, "My Account header should be visible after successful login");
            Console.WriteLine("Login test completed successfully");
        }

        [Then(@"I should see the email input field")]
        public void ThenIShouldSeeTheEmailInputField()
        {
            try
            {
                var emailField = Driver.FindElement(By.Id("input-email"));
                Assert.That(emailField.Displayed, Is.True, "Email input field should be visible");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Email input field not found on the page");
            }
        }

        [Then(@"I should see the password input field")]
        public void ThenIShouldSeeThePasswordInputField()
        {
            try
            {
                var passwordField = Driver.FindElement(By.Id("input-password"));
                Assert.That(passwordField.Displayed, Is.True, "Password input field should be visible");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Password input field not found on the page");
            }
        }

        [Then(@"I should see the login button")]
        public void ThenIShouldSeeTheLoginButton()
        {
            try
            {
                var loginButton = Driver.FindElement(By.XPath("//input[@value='Login']"));
                Assert.That(loginButton.Displayed, Is.True, "Login button should be visible");
            }
            catch (NoSuchElementException)
            {
                Assert.Fail("Login button not found on the page");
            }
        }

        [Then(@"I should see the forgotten password link")]
        public void ThenIShouldSeeTheForgottenPasswordLink()
        {
            Assert.That(_loginPage.IsForgottenPasswordLinkDisplayed(), Is.True, "Forgotten password link should be visible");
        }

        [When("I enter wrong password {string}")]
        public void WhenIEnterWrongPassword(string demo)
        {
            _loginPage.EnterPassword(demo);
        }

        [Then("I should see validation message")]
        public void ThenIShouldSeeValidationMessage()
        {
            var s = _loginPage.InvalidPasswordMsg(" Warning: No match for E-Mail Address and/or Password.") ;
            Assert.That(s, Does.Contain(" Warning: "));
        }
    }
}
