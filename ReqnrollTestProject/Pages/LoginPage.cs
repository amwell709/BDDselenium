using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace ReqnrollSeleniumDemo.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // Constructor
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // Page URL
        private const string PageUrl = "https://ecommerce-playground.lambdatest.io/index.php?route=account/login";

        // Locators
        private By EmailInput => By.Id("input-email");
        private By PasswordInput => By.Id("input-password");
        private By LoginButton => By.XPath("//input[@value='Login']");
        private By MyAccountHeader => By.XPath("//h2[text()='My Account']");
        private By ForgottenPasswordLink => By.XPath("//a[text()='Forgotten Password']");
        private By ErrorMessage => By.CssSelector(".alert-danger");
        private By InvalidPasswordMessage = By.XPath("//*[contains(text(), 'Warning: No match for E-Mail Address and/or Password.'");


        // Page Actions
        public void NavigateToLoginPage()
        {
            _driver.Navigate().GoToUrl(PageUrl);
        }

        public void EnterEmail(string email)
        {
            _wait.Until(driver => driver.FindElement(EmailInput).Displayed);
            _driver.FindElement(EmailInput).Clear();
            _driver.FindElement(EmailInput).SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            _driver.FindElement(PasswordInput).Clear();
            _driver.FindElement(PasswordInput).SendKeys(password);
        }

        public void ClickLoginButton()
        {
            _driver.FindElement(LoginButton).Click();
        }

        public void Login(string email, string password)
        {
            EnterEmail(email);
            EnterPassword(password);
            ClickLoginButton();
        }

        public string InvalidPasswordMsg(string s)
        {
            var el = _driver.FindElement(InvalidPasswordMessage);
            return el.Text;
        }

        public bool IsMyAccountHeaderDisplayed()
        {
            try
            {
                _wait.Until(driver => driver.FindElement(MyAccountHeader).Displayed);
                return _driver.FindElement(MyAccountHeader).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsForgottenPasswordLinkDisplayed()
        {
            try
            {
                return _driver.FindElement(ForgottenPasswordLink).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool IsErrorMessageDisplayed()
        {
            try
            {
                _wait.Until(driver => driver.FindElement(ErrorMessage).Displayed);
                return _driver.FindElement(ErrorMessage).Displayed;
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public string GetErrorMessage()
        {
            return _driver.FindElement(ErrorMessage).Text;
        }

        public string GetPageTitle()
        {
            return _driver.Title;
        }

        public string GetCurrentUrl()
        {
            return _driver.Url;
        }

        public bool IsLoginSuccessful()
        {
            // Check if URL changed to account page after login
            return _driver.Url.Contains("route=account/account") || IsMyAccountHeaderDisplayed();
        }
    }
}
