using NUnitDemo.Constants;
using NUnitDemo.Utilities;
using OpenQA.Selenium;
using System.Net.Http;

namespace NUnitDemo.Pages
{
    public class DevBGInitialPage
    {
        public DevBGInitialPage(IWebDriver driver)
        {
            this.Driver = driver;
        }

        public By AutomationQASectionAnchor => By.CssSelector("a[href*='automation-qa']");
        public By AutomationQAJobBoardHeader => By.CssSelector("h1.miniboard-title span");
        public By AcceptCookiesButton => By.CssSelector("button[data-cky-tag='accept-button']");

        public async Task NavigateToDevBGWebSite()
        {
            TestContext.WriteLine($"Navigate to URL '{TestConstants.DevBGBaseUrl}'");
            await this.Driver.Navigate().GoToUrlAsync(TestConstants.DevBGBaseUrl);
        }

        public void ClickToAcceptCookies()
        {
            TestContext.WriteLine($"Click on button with locator '{this.AcceptCookiesButton}'");
            this.Driver.WaitToGetClickable(this.AcceptCookiesButton).Click();
        }

        public void ClickOnAutomationQASection()
        {
            TestContext.WriteLine($"Click on button with locator '{this.AutomationQASectionAnchor}'");
            this.Driver.WaitToGetClickable(this.AutomationQASectionAnchor).Click();
        }

        public string GetAutonationQAJobBoardHeaderText()
        {
            TestContext.WriteLine($"Get text on locator '{this.AutomationQAJobBoardHeader}'");
            return this.Driver.WaitToBeVisible(this.AutomationQAJobBoardHeader).Text;
        }

        private IWebDriver Driver;
    }
}
