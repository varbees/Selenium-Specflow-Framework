using OpenQA.Selenium;
using Selenium.Specflow.Automation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Specflow.Automation.Pages
{
    public class HomePage
    {

        private readonly IWebDriver _driver;

        private readonly CaptureEvidences _evidences;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _evidences = new CaptureEvidences(driver);
        }

        private By UserInfo => By.XPath("//a[@id='welcome']");
        private By LogoutBtn => By.XPath("//div[@class='panelContainer']//ul//li//a[contains(text(),'Logout')]");
        public bool IsUserLoggedIn() => _driver.IsElementDisplayedWhenVisible(UserInfo);

        public bool IsLoginSuccessful()
        {
            bool loginStatus;
            try
            {
                loginStatus = _driver.IsElementDisplayedWhenVisible(UserInfo);
                _evidences.CaptureLogs(loginStatus, "Login status : " + loginStatus + ".");
            }
            catch (Exception)
            {
                loginStatus = false;
            }
            return loginStatus;
        }

        public bool IsLogoutButtonDisplayed()
        {
            bool logoutBtnDisplayed;
            try
            {
                _driver.ClickElementWhenClickable(UserInfo);
                logoutBtnDisplayed = _driver.IsElementDisplayedWhenVisible(LogoutBtn);
            }
            catch (Exception)
            {
                logoutBtnDisplayed = false;
            }
            return logoutBtnDisplayed;
        }

        public void ClickLogout()
        {
            _driver.ClickElementWhenClickable(LogoutBtn);
        }



    }
}
