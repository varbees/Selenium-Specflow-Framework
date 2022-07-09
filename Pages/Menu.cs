using OpenQA.Selenium;
using Selenium.Specflow.Automation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Specflow.Automation.Pages
{
    public class Menu
    {
        //locators for menu options
        private By AdminMenuItem = By.XPath("//a[@id='menu_admin_viewAdminModule']");
        private By PIMMenuItem = By.XPath("//a[@id='menu_pim_viewPimModule']");
        private By LeaveMenuItem = By.XPath("//a[@id='menu_leave_viewLeaveModule']");
        private By TimeMenuItem = By.XPath("//a[@id='menu_time_viewTimeModule']");
        private By RecruitementMenuItem = By.XPath("//a[@id='menu_recruitment_viewRecruitmentModule']");
        private By MyInfoMenuItem = By.XPath("//a[@id='menu_pim_viewMyDetails']");
        private By PerformanceMenuItem = By.XPath("//a[@id='menu__Performance']");
        private By DashboardMenuItem = By.XPath("//a[@id='menu_dashboard_index']");
        private By DirectoryMenuItem = By.XPath("menu_directory_viewDirectory");
        private By MaintanenceMenuItem = By.XPath("//a[@id='menu_maintenance_purgeEmployee']//b");
        private By PurgeRecordsItem = By.XPath("//a[@id='menu_maintenance_PurgeRecords']");
        private By PurgeCandidateRecords = By.XPath("//li[@class='selected']//ul//li//a[@id='menu_maintenance_purgeCandidateData']");
        private By AccessRecords = By.XPath("//a[@id='menu_maintenance_accessEmployeeData']");
        private By PurgeRecordsTab = By.XPath("//li//a[@id='menu_maintenance_purgeCandidateData']");
        private readonly IWebDriver _driver;

        public Menu(IWebDriver driver)
        {
            _driver = driver;
        }

        public void HoverOnAdminSubMenuItems()
        {
            _driver.MoveToElement(AdminMenuItem);
        }

        public void HoverOnPimSubMenuItem()
        {
            _driver.MoveToElement(PIMMenuItem);
        }

        public void HoverOnLeaveSubMenuItem()
        {
            _driver.MoveToElement(LeaveMenuItem);
        }
        
        public void HoveOnTimeSubMenuItem()
        {
            _driver.MoveToElement(TimeMenuItem);
        }

        public void HoverOnRecruitementSubMenuItem()
        {
            _driver.MoveToElement(RecruitementMenuItem);
        }

        public void HoverOnPerformanceMenuItem()
        {
            _driver.MoveToElement(PerformanceMenuItem);
        }

        public void HoverOnMaintanenceItem()
        {
            _driver.MoveToElement(MaintanenceMenuItem);
        }

        public void HoverOnPurgeRecords()
        {
            HoverOnMaintanenceItem();
            _driver.ClickElementWhenClickable(PurgeRecordsItem);
        }

        public void ClickCandidateRecords()
        {
            _driver.ClickElementWhenClickable(PurgeRecordsTab);
        }

        public void ClickPurgeCandidateRecords()
        {
            _driver.ClickFirstElementWhenVisible(PurgeCandidateRecords);
        }

        public void ClickAccessRecords()
        {
            _driver.ClickElementWhenClickable(AccessRecords);
        }

    }

}
