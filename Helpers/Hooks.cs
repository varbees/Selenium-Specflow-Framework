using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;
using AventStack.ExtentReports.Gherkin.Model;
using Selenium.Specflow.Automation.Utilities;

namespace Selenium.Specflow.Automation.Helpers
{
    [Binding]
    public sealed class Hooks
    {
        private readonly DriverHelper _driverHelper;
        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;
        private static ExtentTest _scenario;
        private CaptureEvidences _evidences;
        private static ExtentTest _feature;
        public static ExtentReports _extent;
        public ExcelReader _dataEx;
         
        public Hooks(DriverHelper driverHelper, ScenarioContext scenarioContext, FeatureContext featureContext, ExcelReader excelHelper)
        {
            _driverHelper = driverHelper;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _dataEx = excelHelper;
        }

        [BeforeTestRun]
        [Obsolete]
        public static void BeforeTestRun()
        {
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string rootPath = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net6.0", "");
            string path = rootPath + @"Reports\Mindfire_Automation_Report_" + currentDateTime + ".html";
            ExtentV3HtmlReporter htmlReporter = new(path);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;
            _extent = new ExtentReports();
            //temp site to display - needs to import from Settings
            _extent.AddSystemInfo("Host Name", Settings.ClientUrl);
            _extent.AddSystemInfo("Environment", "Test");
            _extent.AttachReporter( htmlReporter);
        }

        [BeforeFeature]
        [Obsolete]
        public static void BeforeFeature()
        {
            //evidence logs
            _feature = _extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
            CaptureEvidences.log = new List<string>();
            _extent.AddTestRunnerLogs("<p style=\"color:GREEN; text-align:center;font-size:18px;font-weight:bold;\">" +FeatureContext.Current.FeatureInfo.Title + "</p>");
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            ChromeOptions option = new ChromeOptions();
            option.AddArguments("--start-maximized");
            option.AddArguments("--ignore-ssl-errors=yes");
            option.AddArguments("--ignore-certificate-errors");
            option.AddUserProfilePreference("download.default_directory", Settings.TempDownDirectory);
            option.AddUserProfilePreference("download.prompt_for_download", false);
            option.AddUserProfilePreference("disable-popup-blocking", "true");
            option.AddUserProfilePreference("profile.password_manager_enabled", false);
            //TODO: WebdriverManager For Generic Implementation
            //new DriverManager().SetupDriver(new ChromeConfig());
            _driverHelper.driver = new ChromeDriver(option);
            _driverHelper.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _driverHelper.driver.Manage().Window.Maximize();
            _evidences = new CaptureEvidences(_driverHelper.driver);
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string rootPath = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net6.0", "");

            //in before feature
            //string fileName = Path.Combine(Environment.CurrentDirectory.ToString(), "TestData/Excel.xlsx");
            //ExcelUtil.excelToDataTable(fileName);

            //get excel data into _dataEx
            //_dataEx = ExcelUtil.getRowData("Data", "Data Source", "titleOrId");

            try
            {
                CaptureEvidences.screenshotsPath = Directory.CreateDirectory(rootPath + @"\Screenshots\" + _featureContext.FeatureInfo.Title + "\\" + _scenarioContext.ScenarioInfo.Tags[0] + "_" + currentDateTime).FullName;
            }
            catch (PathTooLongException)
            {
                CaptureEvidences.screenshotsPath = Directory.CreateDirectory(rootPath + @"\Screenshots\" + _featureContext.FeatureInfo.Title + "\\" + _scenarioContext.ScenarioInfo.Title[..10] + "_" + currentDateTime).FullName;
            }
            _scenario = _feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
            _extent.AddTestRunnerLogs("<p style=\"color:BLUE;font-size:16px;font-weight:bold;\">" + "Scenario: " + _scenarioContext.ScenarioInfo.Title);
        }

        [AfterStep]
        public void InsertReportingSteps()
        {

            var stepType = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
            if (_scenarioContext.TestError == null)
            {
                switch (stepType)
                {
                    case "Given":
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case "And":
                        _scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case "When":
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                    case "Then":
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text);
                        break;
                }
            }
            else if (_scenarioContext.TestError != null)
            {
                switch (stepType)
                {
                    case "Given":
                        _scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        _scenario.AddScreenCaptureFromPath(_evidences.getScreenshot());
                        break;
                    case "And":
                        _scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        _scenario.AddScreenCaptureFromPath(_evidences.getScreenshot());
                        break;
                    case "When":
                        _scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        _scenario.AddScreenCaptureFromPath(_evidences.getScreenshot());
                        break;
                    case "Then":
                        _scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                        _scenario.AddScreenCaptureFromPath(_evidences.getScreenshot());
                        break;
                }
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {   
            int logNumber = 1;
            foreach(string logs in CaptureEvidences.log)
            {
                _extent.AddTestRunnerLogs("<br>"+logNumber + ". " + logs);
                logNumber++;
            }
            CaptureEvidences.log.Clear();
            //_driverHelper.driver.Close();
            _driverHelper.driver.Quit();
            _driverHelper.driver.Dispose();
            //CleanUpChromeInstance();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extent.Flush();
        }

        private static void CleanUpChromeInstance()
        {
            Process[] ChromeDriverInstances = Process.GetProcessesByName("chromedriver");
            CleanUpProcess(ChromeDriverInstances);
        }

        private static void CleanUpProcess(Process[] processes)
        {
            foreach (Process process in processes)
            {
                process.Kill();
                process.CloseMainWindow();
                process.Close();
            }
        }
    }
}
