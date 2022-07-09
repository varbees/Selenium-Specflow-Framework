using NUnit.Framework;
using Selenium.Specflow.Automation.Helpers;
using Selenium.Specflow.Automation.Pages;

namespace Selenium.Specflow.Automation.StepDefinitions
{   
    [Binding]
    public class LoginStepDefinitions
    {
        private DriverHelper _driverHelper;
        LoginPage loginPage;
        HomePage homePage;

        public LoginStepDefinitions(DriverHelper driverHelper)
        {
            _driverHelper = driverHelper;
            loginPage = new LoginPage(_driverHelper.driver);
            homePage = new HomePage(_driverHelper.driver);
        }


        [StepDefinition(@"the user navigates to login page")]
        public void GivenTheUserNavigatesToLoginPage()
        {
            loginPage.Login();
        }

        [StepDefinition(@"Logout button is present in the default home page")]
        public void GivenLogoutButtonIsPresentInTheDefaultHomePage()
        {
            loginPage.Login();
            loginPage.EnterUseNameAndPassword(Settings.Username, Settings.Password);
            loginPage.ClickLogin();
            Assert.IsTrue(homePage.IsLogoutButtonDisplayed(), "Error: Logout Button not displayed");
        }

        [StepDefinition(@"the user clicks on the Logout button")]
        public void WhenTheUserClicksOnTheLogoutButton()
        {
            homePage.ClickLogout();
        }

        [Then(@"Login page should be visible to the user")]
        public void ThenLoginPageShouldBeVisibleToTheUser()
        {
            Assert.Multiple(()=>
            {
                Assert.IsTrue(loginPage.IsLoginPageheaderDisplayed());
                Assert.IsTrue(loginPage.IsLoginDetailsDisplayed());
            });
        }


        [StepDefinition(@"user should be able to view the Username, Password and Login elements on the screen")]
        public void ThenUserShouldBeAbleToViewTheUsernamePasswordAndLoginElementsOnTheScreen()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(loginPage.IsUsernameTextDisplayed());
                Assert.IsTrue(loginPage.IsPasswordTextDisplayed());
                Assert.IsTrue(loginPage.IsLoginButtonDisplayed());
            });
        }


        [StepDefinition(@"the user enters valid Username and valid password in the Username and password field")]
        public void WhenTheUserEntersValidUsernameAndValidPasswordInTheUsernameAndPasswordField()
        { 
            loginPage.EnterUseNameAndPassword(Settings.Username, Settings.Password);
        }

        [StepDefinition(@"the user enters invalid Username and valid password in the Username and password field")]
        public void WhenTheUserEntersInvalidUsernameAndValidPasswordInTheUsernameAndPasswordField()
        {
            loginPage.EnterUseNameAndPassword("User", Settings.Password);
        }

        [StepDefinition(@"clicks on login button")]
        public void WhenClicksOnLoginButton()
        {
            loginPage.ClickLogin();
        }

        [StepDefinition(@"user should navigate successfully to the default home page")]
        public void ThenUserShouldNavigateSuccessfullyToTheDefaultHomePage()
        {
           Assert.IsTrue(homePage.IsLoginSuccessful());
        }

        [StepDefinition(@"user should be able to see Error Message of Invalid credentials on the screen")]
        public void ThenUserShouldBeAbleToSeeErrorMessageOfInvalidCredentialsOnTheScreen()
        {
            Assert.IsTrue(loginPage.IsErrorMessageDisplayed());
        }

    }

}
