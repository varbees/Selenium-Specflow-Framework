using OpenQA.Selenium;
using Selenium.Specflow.Automation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Specflow.Automation.Pages
{
    public class MaintanencePage
    {
        private readonly IWebDriver _driver;
        private readonly CaptureEvidences _evidences;

        public MaintanencePage(IWebDriver driver)
        {
            _driver = driver;
            _evidences = new CaptureEvidences(driver);
        }

        private By Downlaodheader = By.XPath("//label[@for='confirm_password']");
        private By PasswdField = By.XPath("//input[@id='confirm_password']");
        private By SubmitBtn = By.XPath("//input[@type='submit']");
        private By InputEmpName = By.XPath("//input[@id='employee_empName']");
        private By EmpDropDownListFirstItem = By.XPath("//div[@class='ac_results']//ul//li[1]");
        private By SearchBtn = By.XPath("//input[@value='Search']");
        private By SelectedCandidatesHeader = By.XPath("//div[@class='head']//h1[contains(text(),'Selected Candidates')]");
        private By EmpId = By.XPath("//label//span[contains(text(),'Employee Id')]/../../input[@id='first_name']");
        private By EmployeeRecords = By.XPath("//label[@for='employee']/following-sibling::input[@id='employee_empName']");
        private By SuccessMsg = By.XPath("//div[@class='message success fadable']");
        private By InputCandidateType = By.XPath("//input[@id='candidate_empName']");
        private By VacancyDrpDown = By.XPath("//ul//li[@class='ac_even ac_over']");
        private By PurgeBtn = By.XPath("//input[@id='btnPurge']");
        private By PurgeConfirm = By.XPath("//input[@id='modal_confirm']");
        private By PurgeCancel = By.XPath("//input[@class='btn cancel']");

        public IWebElement TxtPassword => WebDriverExtensions.GetElementWhenVisible(_driver, PasswdField);
        public IWebElement EmpName => WebDriverExtensions.GetElementWhenVisible(_driver, InputEmpName);
        public IWebElement SelectVacancy => WebDriverExtensions.GetElementWhenVisible(_driver, InputCandidateType);


        public bool IsVerifyPasswordDisplayed() => _driver.IsElementDisplayedWhenVisible(Downlaodheader);
        public bool IsEmployeeIdVisible() => _driver.IsElementDisplayedWhenVisible(EmpId);
        public bool IsSuccessMsgDisplayed() => _driver.IsElementDisplayedWhenVisible(SuccessMsg);
        public bool IsSelectedCandidatesVisible() => _driver.IsElementDisplayedWhenVisible(SelectedCandidatesHeader);

        public void EnterPasswordAndSubmit(string passwd)
        {
            if (!string.IsNullOrEmpty(passwd))
            {
                TxtPassword.SendKeys(passwd);
                _driver.ClickElementWhenClickable(SubmitBtn);
            }
        }

        public void ClickSearchBtn()
        {
            _driver.ClickElementWhenClickable(SearchBtn);
        }

        public void ClickPurgeBtn()
        {
            _driver.ClickElementWhenClickable(PurgeBtn);
        }

        public void ClickPurgeOption(string purgeOption)
        {   if (purgeOption == "Purge")
            {
                _driver.ClickElementWhenClickable(PurgeConfirm);
            }else if(purgeOption == "Cancel")
            {
                _driver.ClickElementWhenClickable(PurgeCancel);
            }
            else
            {
                Console.WriteLine("Error: Purge Otpion - " + purgeOption + " not available");
            }
        }

        public void EnterNameAndSearch(string employeeeName)
        {
            EmpName.SendKeys(employeeeName);
            _driver.ClickElementWhenExists(EmpDropDownListFirstItem);
            _driver.ClickElementWhenClickable(SearchBtn);
            _evidences.CaptureLogs(true, "Searching Employee Details");
        }

        public bool isEmployeeRecordsVisible()
        {
            bool empRecordsPage;
            try
            {
               return _driver.IsElementDisplayedWhenVisible(EmployeeRecords);
            }
            catch
            {
                empRecordsPage = false;
            }
            return empRecordsPage;
        }

        public bool IsVacancyRecordsVisible()
        {
            bool vacancyRecords;
            try
            {
                return _driver.IsElementDisplayedWhenVisible(PurgeBtn);
            }
            catch
            {
                vacancyRecords = false;
            }
            return vacancyRecords;
        }

        public void SelectVacancyFromDropDown(string vacancy)
        {
            try
            {
                SelectVacancy.SendKeys(vacancy);
                _driver.ClickElementWhenExists(VacancyDrpDown);
                _evidences.CaptureLogs(true, "Selected" + vacancy + "for purging " );
            }
            catch
            {
                _evidences.CaptureLogs(false, "Selected" + vacancy + "does not exist");
            }
        }
    }
}
