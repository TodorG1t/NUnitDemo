using NUnit.Framework.Internal;
using NUnitDemo.Common;
using NUnitDemo.Constants;
using NUnitDemo.Models;
using NUnitDemo.Pages;

namespace NUnitDemo.Fixtures.UiFixtures
{
    [TestFixture]
    public class DevBGJobsFixture : BaseDriver
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            this.DevBGInitialPage = new DevBGInitialPage(this.Driver);
            this.AutomationQAJobsPage = new AutomationQAJobsPage(this.Driver, this.Actions);
        }

        [Test]
        [TestCase(TestName = "Get current Automation QA positions in DevBG", Description = "TestName is sed just for better naming")]
        public async Task GetCurrentAutomationQAPositionsInDevBG()
        {
            await this.DevBGInitialPage.NavigateToDevBGWebSite();
            this.DevBGInitialPage.ClickToAcceptCookies();
            this.DevBGInitialPage.ClickOnAutomationQASection();

            this.AutomationQAJobsPage.SelectJobLocationSofia();
            this.AutomationQAJobsPage.SelectJobLocationRemote();
            this.AutomationQAJobsPage.SelectProductCompanyCategory();

            List<JobItemModel> listJobsData = new List<JobItemModel>();

            do
            {
                listJobsData.AddRange(this.AutomationQAJobsPage.GetAllJobsItemDataPerPage());
                this.AutomationQAJobsPage.ClickNextPageButtonIfPresent();
            }
            while (this.AutomationQAJobsPage.NextPageButtonStatusCondition());

            listJobsData.AddRange(this.AutomationQAJobsPage.GetAllJobsItemDataPerPage());

            TestContext.WriteLine("");
            TestContext.WriteLine($"-================= Today open positions =================-");
            foreach (var jobData in listJobsData)
            {
                TestContext.WriteLine($"{jobData.CompanyName}: {jobData.Position}");
            }
            TestContext.WriteLine($"-==================================-");
            TestContext.WriteLine("");

            Assert.Multiple(() =>
            {
                Assert.That(listJobsData.Count(),
                    Is.EqualTo(this.AutomationQAJobsPage.GetTotalNumberOfJobs()),
                    "Number of jobs got through page navigation is different than pointed total number of jobs!");
                Assert.That(
                    this.DevBGInitialPage.GetAutonationQAJobBoardHeaderText(),
                    Is.EqualTo(TestConstants.AutomationQABoardTitle),
                    $"Automaton QA Job board title is not '{TestConstants.AutomationQABoardTitle}'");
            });
        }

        private AutomationQAJobsPage AutomationQAJobsPage { get; set; }
        private DevBGInitialPage DevBGInitialPage { get; set; }
    }
}
