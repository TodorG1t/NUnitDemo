using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace NUnitDemo.Utilities
{
    public static class DriverUtilities
    {
        public static IWebElement WaitToGetClickable(this IWebDriver driver, By selector, int timeoutSeconds = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
            return wait.Until(drv =>
            {
                var el = drv.FindElement(selector);
                return (el.Displayed && el.Enabled) ? el : null;
            });
        }

        public static IWebElement WaitToBeVisible(this IWebDriver driver, By selector, int timeout = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            return wait.Until(drv =>
            {
                var el = drv.FindElement(selector);
                return el.Displayed ? el : null;
            });
        }

        public static IReadOnlyCollection<IWebElement> WaitAllToBeVisible(this IWebDriver driver, By selector, int timeout = 10)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            return wait.Until(drv =>
            {
                var elements = drv.FindElements(selector);

                if (elements.Count > 0 && elements.All(el => el.Displayed))
                {
                    return elements;
                }

                return null;
            });
        }

        public static void ClickElementIfPresent(this IWebDriver driver, By selector, int timeoutInSeconds = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                var element = wait.Until(driver => driver.FindElement(selector));
                if (element != null && element.Displayed && element.Enabled)
                {
                    element.Click();
                }
            }
            catch (WebDriverTimeoutException)
            {
                // Element not found within timeout – silently ignore
            }
            catch (NoSuchElementException)
            {
                // Element truly not found – silently ignore
            }
            catch (ElementNotInteractableException)
            {
                // Element is there but cannot be clicked - silently ignore
            }
        }

        public static bool GetElementIsPresentStatus(this IWebDriver driver, By selector, int timeout = 5)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            try
            {
                return wait.Until(drv =>
                {
                    try
                    {
                        var el = drv.FindElement(selector);
                        return el.Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                        return false;
                    }
                    catch (StaleElementReferenceException)
                    {
                        return false;
                    }
                });
            }
            catch (WebDriverTimeoutException)
            {
                return false;
            }
        }

        public static bool IsBodyBeforePseudoElementVisible(this IWebDriver driver)
        {
            var js = (IJavaScriptExecutor)driver;
            string script = @"
                var pseudo = window.getComputedStyle(document.body, '::before');
                return pseudo &&
                    pseudo.getPropertyValue('content') !== 'none' &&
                    pseudo.getPropertyValue('display') !== 'none' &&
                    pseudo.getPropertyValue('visibility') !== 'hidden' &&
                    parseFloat(pseudo.getPropertyValue('opacity')) > 0;
            ";

            var result = js.ExecuteScript(script);

            return result is bool boolResult && boolResult;
        }

        public static void WaitUntilPseudoElementIsNotPresent(this IWebDriver driver, int timeout = 10)
        {
            TestContext.WriteLine($"Wait site loader to disappear");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));
            wait.Until(d => !IsBodyBeforePseudoElementVisible(d));
        }
    }
}
