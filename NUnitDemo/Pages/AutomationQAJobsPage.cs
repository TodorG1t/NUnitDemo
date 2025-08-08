using NUnitDemo.Models;
using NUnitDemo.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Text.RegularExpressions;

namespace NUnitDemo.Pages
{
    public class AutomationQAJobsPage
    {
        public AutomationQAJobsPage(
            IWebDriver driver,
            Actions actions)
        {
            this.Driver = driver;
            this.Actions = actions;
        }

        public By SofiaJobLocationCheckbox => By.CssSelector("div[data-name=\"job_location\"] div[data-value=\"sofiya\"]");
        public By RemoteJobLocationCheckbox => By.CssSelector("div[data-name=\"job_location\"] div[data-value=\"remote\"]");
        public By ProductCompanyCategoryCheckbox => By.CssSelector("div[data-name=\"company_category\"] div");
        public By JobPositionItemParentElement => By.CssSelector("div.job-list-item");
        public By JobPositionTitleHeaderText => By.CssSelector("div.job-list-item h6");
        public By JobPositionCompanyNameText => By.CssSelector("div.job-list-item div.company-logo-wrap span");
        public By NextPageButton => By.CssSelector("div.facetwp-pager a.next");

        public By TotalNumberOfJobs => By.CssSelector("div[data-name=\"total_items\"]");

        public void SelectJobLocationSofia()
        {
            this.Driver.WaitUntilPseudoElementIsNotPresent();

            TestContext.WriteLine($"Click on button with locator '{this.SofiaJobLocationCheckbox}'");
            this.Driver.WaitToGetClickable(this.SofiaJobLocationCheckbox).Click();
        }

        public void SelectJobLocationRemote()
        {
            this.Driver.WaitUntilPseudoElementIsNotPresent();

            TestContext.WriteLine($"Click on button with locator '{this.RemoteJobLocationCheckbox}'");
            this.Driver.WaitToGetClickable(this.RemoteJobLocationCheckbox).Click();
        }

        public void SelectProductCompanyCategory()
        {
            this.Driver.WaitUntilPseudoElementIsNotPresent();

            TestContext.WriteLine($"Scroll to element with locator '{this.ProductCompanyCategoryCheckbox}'");
            this.Actions.ScrollToElement(this.Driver.WaitToBeVisible(this.ProductCompanyCategoryCheckbox));

            TestContext.WriteLine($"Click on button with locator '{this.ProductCompanyCategoryCheckbox}'");
            this.Driver.WaitToGetClickable(this.ProductCompanyCategoryCheckbox).Click();
        }

        public List<JobItemModel> GetAllJobsItemDataPerPage()
        {
            this.Driver.WaitUntilPseudoElementIsNotPresent();

            var listItemDetails = new List<JobItemModel>();

            TestContext.WriteLine($"Wait all elements with locator '{this.JobPositionItemParentElement}' to be visible");
            var jobItemsPerPage = this.Driver.WaitAllToBeVisible(this.JobPositionItemParentElement);

            foreach (var jobItem in jobItemsPerPage)
            {
                var itemData = new JobItemModel
                {
                    Position = jobItem.FindElement(By.CssSelector("h6")).Text,
                    CompanyName = jobItem.FindElement(By.CssSelector("div.company-logo-wrap span.company-name")).Text
                };

                listItemDetails.Add(itemData);
            }

            return listItemDetails;
        }

        public void ClickNextPageButtonIfPresent()
        {
            this.Driver.WaitUntilPseudoElementIsNotPresent();

            TestContext.WriteLine($"Click on button with locator '{this.NextPageButton}'");
            this.Driver.ClickElementIfPresent(this.NextPageButton);
        }

        public bool NextPageButtonStatusCondition()
        {
            this.Driver.WaitUntilPseudoElementIsNotPresent();

            TestContext.WriteLine($"Get element status with locator '{this.NextPageButton}'");
            return this.Driver.GetElementIsPresentStatus(this.NextPageButton);
        }

        public int GetTotalNumberOfJobs()
        {
            TestContext.WriteLine($"Get element text value with locator '{this.TotalNumberOfJobs}'");
            string totalJobsHeader = this.Driver.WaitToBeVisible(this.TotalNumberOfJobs).Text;
            Match match = Regex.Match(totalJobsHeader, @"\d+");
            int totalNumberOfJobs = int.Parse(match.Value);

            return totalNumberOfJobs;
        }

        private IWebDriver Driver;
        private Actions Actions;
    }
}
