using OpenQA.Selenium;
using Selenium.Specflow.Automation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Specflow.Automation.Pages
{
    public class LoginPage
    {

        private readonly IWebDriver _driver;
        private readonly CaptureEvidences _evidences;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _evidences = new CaptureEvidences(driver);

        }

        private By LoginPageContent => By.XPath("//div[@id='divLogin']");
        private By LoginDetails => By.XPath("//span[contains(text(),'Admin')]");
        private By LoginUserName => By.XPath("//input[@id='txtUsername']");
        private By LoginPassword => By.XPath("//input[@id='txtPassword']");
        private By LoginButton => By.XPath("//input[@type='submit']");
        private By UserInfo => By.XPath("//a[@id='welcome']");
        private By InvalidCredentialsMessage => By.XPath("//span[contains(text(),'Invalid credentials')]");

        public IWebElement TxtUserName => WebDriverExtensions.GetElementWhenVisible(_driver, LoginUserName);
        public IWebElement TxtPassword => WebDriverExtensions.GetElementWhenVisible(_driver, LoginPassword);

        public bool IsLoginPageheaderDisplayed() => _driver.IsElementDisplayedWhenVisible(LoginPageContent);
        public bool IsLoginDetailsDisplayed() => _driver.IsElementDisplayedWhenVisible(LoginDetails);
        public bool IsUsernameTextDisplayed() => _driver.IsElementDisplayedWhenVisible(LoginUserName);
        public bool IsPasswordTextDisplayed() => _driver.IsElementDisplayedWhenVisible(LoginPassword);
        public bool IsLoginButtonDisplayed() => _driver.IsElementDisplayedWhenVisible(LoginButton);


        public void Login()
        {
            _driver.Navigate().GoToUrl(Settings.ClientUrl);
            WebDriverExtensions.WaitForPageToLoad(_driver);
            _evidences.CaptureLogs(true, "Navigated to login page.");
        }

        public bool IsErrorMessageDisplayed()
        {
            bool errorStatus = _driver.IsElementDisplayedWhenVisible(InvalidCredentialsMessage);
            _evidences.CaptureLogs(true, "Invalid login credentials entered.");
            return errorStatus;
        }

        public void EnterUseNameAndPassword(string userName, string password)
        {
            TxtUserName.SendKeys(userName);
            TxtPassword.SendKeys(password);
        }

        public void ClickLogin()
        {
            _driver.MoveToElement(LoginButton);
            _driver.ClickElementWhenExists(LoginButton); 
        }      

    }
}
