using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace NUnitDemo.Common
{
    [TestFixture]
    public class BaseDriver
    {
        [OneTimeSetUp]
        public void Init()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--window-size=1920,1080");

            this.Driver = new ChromeDriver(options);
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(150)
            };
            this.Actions = new Actions(this.Driver);
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            this.Driver.Close();
            this.Driver.Quit();
        }

        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }
        public Actions Actions { get; set; }
    }
}
