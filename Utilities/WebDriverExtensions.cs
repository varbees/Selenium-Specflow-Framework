using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using System.Drawing;

namespace Selenium.Specflow.Automation.Utilities
{
    public static class WebDriverExtensions
    {
        public static void DragAndDropElement(this IWebDriver driver, By elementToDragLocator, By elementToDropOnLocator)
        {
            void WebDriverActions()
            {
                IWebElement elementToDrag = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementToDragLocator));
                IWebElement elementToDropOnto = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(elementToDropOnLocator));

                new Actions(driver)
                    .ClickAndHold(elementToDrag)
                    .MoveToElement(elementToDropOnto)
                    .Release(elementToDrag)
                    .Build()
                    .Perform();
            }
            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void ClickElementWhenClickable(this IWebDriver driver, By locator)
        {
            void WebDriverActions()
            {
                IWebElement elementToClick = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
                new Actions(driver)
                    .MoveToElement(elementToClick)
                    .Click()
                    .Perform();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void ClickElementWhenExists(this IWebDriver driver, By locator)
        {
            void WebDriverActions()
            {
                IWebElement elementToClick = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));

                new Actions(driver)
                    .MoveToElement(elementToClick)
                    .Click()
                    .Perform();

            }
            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);

        }

        public static void ClickFirstElementWhenVisible(this IWebDriver driver, By locator)
        {
            void WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));

                ReadOnlyCollection<IWebElement> elements = driver.FindElements(locator);
                elements.First().Click();

            }
            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);

        }

        public static void ClickLastElementWhenVisible(this IWebDriver driver, By locator)
        {
            void WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                ReadOnlyCollection<IWebElement> elements = driver.FindElements(locator);
                elements.Last().Click();

            }
            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);

        }

        public static void ClickNoElement(this IWebDriver driver)
        {
            driver.ClickElementWhenExists(By.XPath("//body"));
        }

        public static void ClickNthElementWhenVisible(this IWebDriver driver, By locator, int index)
        {
            void WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                ReadOnlyCollection<IWebElement> elements = driver.FindElements(locator);
                elements[index].Click();

            }
            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);

        }

        public static IList<IWebElement> GetElementsWhenExists(this IWebDriver driver, By locator)
        {
            driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
            return driver.FindElements(locator);
        }

        public static string[] GetElementsTextWhenVisible(this IWebDriver driver, By locator)
        {
            string[] WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                return driver.FindElements(locator).Select(e => e.Text.Trim()).ToArray();
            }
            return FunctionRetrier.RetryOnException<string[], StaleElementReferenceException>(WebDriverActions);
        }

        public static string[] GetElementTextsIfExists(this IWebDriver driver,By locator)
        {
            string[] WebDriverActions()
            {
                driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
                return driver.FindElements(locator).Select(e => e.Text.Trim()).ToArray();
            }

            try
            {
                return FunctionRetrier.RetryOnException<string[], StaleElementReferenceException>(WebDriverActions);
            }
            catch (WebDriverTimeoutException webDriverTimeOut) when (webDriverTimeOut.InnerException is NoSuchElementException)
            {
                return Array.Empty<string>();
            }
        }

        public static IWebElement GetElementWhenExists(this IWebDriver driver, By locator)
        {
            return driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));
        }

        public static IWebElement GetElementWhenVisible(this IWebDriver driver, By locator)
        {
            return driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public static string GetElementAttributeWhenVisible(this IWebDriver driver, By locator, string attribute)
        {
            string WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).GetAttribute(attribute);
            return FunctionRetrier.RetryOnException<string, StaleElementReferenceException>(WebDriverActions);
        }

        public static Point GetElementLocationWhenVisible(this IWebDriver driver, By locator)
        {
            Point WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Location;
            return FunctionRetrier.RetryOnException<Point, StaleElementReferenceException>(WebDriverActions);
        }

        public static int GetDisplayedElementCount(this IWebDriver driver, By locator)
        {
            int WebDriverActions()
            {
                try
                {
                    return driver.FindElements(locator).Count(e => e.Displayed);
                }
                catch (NoSuchElementException)
                {
                    return 0;
                }
            }

            return FunctionRetrier.RetryOnException<int, StaleElementReferenceException>(WebDriverActions);
        }

        public static string GetTextBoxValueWhenVisible(this IWebDriver driver, By locator)
        {
            return driver.GetElementAttributeWhenVisible(locator, "value");
        }

        public static string GetElementTextWhenExists(this IWebDriver driver, By locator)
        {
            string WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator)).Text;

            return FunctionRetrier.RetryOnException<string, StaleElementReferenceException>(WebDriverActions);
        }   

        public static string GetElementTextWhenVisible(this IWebDriver driver, By locator)
        {
            string WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Text;

            return FunctionRetrier.RetryOnException<string, StaleElementReferenceException>(WebDriverActions);
        }

        public static bool IsElementDisplayedWhenVisible(this IWebDriver driver, By locator)
        {
            bool isElementDisplayed;
            try
            {
                //skipped StaleElementReferernceException handling for local debugging
                isElementDisplayed = driver.Wait(1).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Displayed;
            }
            catch (Exception ex)
            {
                isElementDisplayed = false;
                Console.WriteLine("Exception: " + ex.Message);
            }
            return isElementDisplayed;
        }

        public static bool IsElementClickable(this IWebDriver driver, By locator)
        {
            bool isElementClickable;
            try
            {
                //skipped StaleElementReferernceException handling for local debugging
                driver.Wait(1).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
                isElementClickable = true;
            }
            catch (WebDriverTimeoutException)
            {
                isElementClickable= false;
            }
            return isElementClickable;
        }

        public static bool IsElementEnabledWhenVisible(this IWebDriver driver, By locator)
        {
            bool WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Enabled;
            return FunctionRetrier.RetryOnException<bool, StaleElementReferenceException>(WebDriverActions);
        }

        public static bool IsElementSelected(this IWebDriver driver, By locator)
        {
            bool WebDriverActions() => driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator)).Selected;
            return FunctionRetrier.RetryOnException<bool, StaleElementReferenceException>(WebDriverActions);
        }

        public static void MoveToElement(this IWebDriver driver, By locator)
        {
            void WebDriverActions()
            {
                new Actions(driver)
                .MoveToElement(driver.GetElementWhenVisible(locator))
                .Perform();
            }
            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void PasteIntoElementWhenExists(this IWebDriver driver, By locator)
        {
            void WebDriverActions()
            {
                IWebElement elementToClick = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(locator));

                new Actions(driver)
                    .MoveToElement(elementToClick)
                    .Click()
                    .KeyDown(Keys.Control)
                    .SendKeys("V")
                    .KeyUp(Keys.Control)
                    .Perform();
            }
            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void RefreshBrowser(this IWebDriver driver, TimeSpan? waitTime = null)
        {
            driver.Navigate().Refresh();

            if (waitTime.HasValue)
            {
                Task.Delay(waitTime.Value).Wait();
            }
        }

        public static void ScrollIntoView(this IWebDriver driver, By targetElementlocator)
        {
            IWebElement targetElement = driver.GetElementWhenExists(targetElementlocator);
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView()", targetElement);
        }

        public static void ScrollToBottom(this IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        public static void ScrollToTop(this IWebDriver driver)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.scrollTo(0, -document.body.scrollHeight)");
        }

        public static void SendKeysWhenVisible(this IWebDriver driver, By locator, string value)
        {
            void WebDriverActions()
            {
                IWebElement element = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));

                new Actions(driver)
                    .MoveToElement(element)
                    .SendKeys(value)
                    .Perform();
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void SetTextBoxValueWhenVisible(this IWebDriver driver, By locator, string value)
        {
            void WebDriverActions()
            {
                IWebElement element = driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                element.Clear();
                element.SendKeys(value);
            }

            FunctionRetrier.RetryOnException<StaleElementReferenceException>(WebDriverActions);
        }

        public static void WaitForPageToLoad(this IWebDriver driver)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(d => ((IJavaScriptExecutor)d)
                .ExecuteScript("return document.readyState").Equals("complete"));
        }

        public static void WaitUntilElementIsVisble(this IWebDriver driver, By locator)
        {
            driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
        }

        public static void WaitUntilElementIsClickable(this IWebDriver driver, By locator)
        {
            driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }

        public static void WaitUntilElementIsInvisible(this IWebDriver driver, By locator)
        {
            driver.Wait().Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
        }

        public static WebDriverWait Wait(this IWebDriver driver, int waitSeconds = 30)
        {
            return new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
        }

    }

}
