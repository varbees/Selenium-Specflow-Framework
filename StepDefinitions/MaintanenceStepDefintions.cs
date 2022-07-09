using NUnit.Framework;
using Selenium.Specflow.Automation.Helpers;
using Selenium.Specflow.Automation.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Specflow.Automation.StepDefinitions
{
    [Binding]
    public class MaintanenceStepDefintions
    {
        private DriverHelper _driverHelper;
        HomePage homePage;
        LoginPage loginPage;
        MaintanencePage maintanencePage;
        Menu menu;

        public MaintanenceStepDefintions(DriverHelper driverHelper)
        {
            _driverHelper = driverHelper;
            loginPage = new LoginPage(_driverHelper.driver);
            homePage = new HomePage(_driverHelper.driver);
            menu = new Menu(_driverHelper.driver);
            maintanencePage = new MaintanencePage(_driverHelper.driver);
        }

        [StepDefinition(@"user is logged in successfully and on homepage")]
        public void GivenUserIsLoggedInSuccessfullyAndOnHomepage()
        {
            homePage.IsUserLoggedIn();
        }


        [StepDefinition(@"user hovers on maintanence option and clicks on Access Records")]
        public void WhenUserHoversOnMaintanenceOptionAndClicksOnAccessRecords()
        {
            menu.HoverOnMaintanenceItem();
            menu.ClickAccessRecords();
        }

        [StepDefinition(@"user hovers on maintanence option and clicks on Candidate Records in Purge Records")]
        public void WhenUserHoversOnMaintanenceOptionAndClicksOnCandidateRecordsInPurgeRecords()
        {
            menu.HoverOnPurgeRecords();
            menu.ClickCandidateRecords();
        }


        [StepDefinition(@"authenicate password option is displayed")]
        public void ThenAuthenicatePasswordOptionIsDisplayed()
        {
            Assert.IsTrue(maintanencePage.IsVerifyPasswordDisplayed(),"Error: Verify Password Not Displayed");   
        }

        [StepDefinition(@"user enters the valid password and clicks on submit button")]
        public void ThenUserEntersThePasswordAndClicksOnSubmitButton()
        {
            maintanencePage.EnterPasswordAndSubmit("admin123");
        }

        [StepDefinition(@"user enters the wrong password and clicks on submit button")]
        public void ThenUserEntersTheWrongPasswordAndClicksOnSubmitButton()
        {
            maintanencePage.EnterPasswordAndSubmit("Admin");
        }

        [StepDefinition(@"user cannot access the employee records")]
        public void ThenUserCannotAccessTheEmployeeRecords()
        {
            Assert.IsFalse(maintanencePage.isEmployeeRecordsVisible(), "Error: Employee records are visible.");
        }


        [StepDefinition(@"user enters employee name and clicks on search button")]
        public void ThenUserEntersEmployeeNameAndClicksOnSearchButton()
        {
            maintanencePage.EnterNameAndSearch("fi");
        }


        [StepDefinition(@"Selected vacancy type should be displayed")]
        public void ThenSelectedVacancyTypeShouldBeDisplayed()
        {
            Assert.IsTrue(maintanencePage.IsSelectedCandidatesVisible(), "Error: Selected candidates not displayed");
        }

        [StepDefinition(@"user clicks on purge records")]
        public void ThenUserClicksOnPurgeRecords()
        {
            maintanencePage.ClickPurgeBtn();
        }

        [StepDefinition(@"user clicks on ""([^""]*)"" in confirmation message")]
        public void ThenUserClicksOnInConfirmationMessage(string purgeStatus)
        {
            Assert.IsTrue(maintanencePage.IsVacancyRecordsVisible(), "Error: Candidate records are not available for purge");
            maintanencePage.ClickPurgeOption(purgeStatus);
        }


        [StepDefinition(@"clicks on Search button")]
        public void ThenClicksOnSearchButton()
        {
            maintanencePage.ClickSearchBtn();
        }

        [StepDefinition(@"user should see employee Id")]
        public void ThenUserShouldSeeEmployeeId()
        {
            Assert.IsTrue(maintanencePage.IsEmployeeIdVisible(), "Error: Employee Id not visible");
        }

        [StepDefinition(@"candidate records or purged successfully")]
        public void ThenCandidateRecordsOrPurgedSuccessfully()
        {
            Assert.IsTrue(maintanencePage.IsSuccessMsgDisplayed(),"Error: Candidate records not purged");
        }

        [StepDefinition(@"candidate records are not purged")]
        public void ThenCandidateRecordsAreNotPurged()
        {
            Assert.IsTrue(maintanencePage.IsVacancyRecordsVisible(), "Error: Candidate records are not visible");
        }


        [Then(@"user selects ""([^""]*)"" from drop down")]
        public void ThenUserSelectsFromDropDown(string vacancyType)
        {
            maintanencePage.SelectVacancyFromDropDown(vacancyType);
        }

    }
}
