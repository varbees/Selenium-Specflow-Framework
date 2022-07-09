using OpenQA.Selenium;

namespace Selenium.Specflow.Automation.Utilities
{
    public class CaptureEvidences
    {
        private IWebDriver driver = null;
        public static List<string> log;
        public static string screenshotsPath;

        public CaptureEvidences(IWebDriver driver)
        {
            this.driver = driver;
        }
        public string getScreenshot()
        {
            string currentDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            string pathPart = AppDomain.CurrentDomain.BaseDirectory.Replace(@"\bin\Debug\net6.0", "");
            if (!Directory.Exists(pathPart + @"Screenshots\Failures\"))
            {
                Directory.CreateDirectory(pathPart + @"Screenshots\Failures\");
            }
            string path = pathPart + @"Screenshots\Failures\" + currentDateTime + ".jpeg";
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            screenshot.SaveAsFile(path, ScreenshotImageFormat.Jpeg);
            Thread.Sleep(2000);
            return path;
        }

        public void CaptureLogs(bool screenshot, string logs = null)
        {
            string currentDateTime = DateTime.Now.ToString("yyyMMddHHmmss");
            // get path from before scenario
            //string path = screenshotsPath + @"\" + currentDateTime + ".jpeg";
            if (logs != null)
            {
                log.Add(logs);
            }
            if (screenshot)
            {
                Screenshot screen = ((ITakesScreenshot)driver).GetScreenshot();
                screen.SaveAsFile(screenshotsPath + @"\" + currentDateTime + ".jpeg", ScreenshotImageFormat.Jpeg);
                Thread.Sleep(2000);
            }
        }
    }
}
