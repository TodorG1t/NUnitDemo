using NUnitDemo.Common;
using NUnitDemo.Fixtures.ApiFixtures.Scenarios;
using System.Net;

namespace NUnitDemo.Fixtures.ApiFixtures
{
    [TestFixture]
    public class JsonPlaceholderFixture
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            client = new ApiClient();
        }

        [Test]
        [TestCaseSource(typeof(ApiRequestScenarios), nameof(ApiRequestScenarios.Positive_RequestScenarios))]
        [TestCaseSource(typeof(ApiRequestScenarios), nameof(ApiRequestScenarios.Negative_RequestScenarios))]
        public async Task ValidateDemoGetRequest(
            HttpMethod httpMethod,
            string requestUrl,
            object body,
            HttpStatusCode expectedStatusCode)
        {
            var response = await client.SendHttpRequestAsync(
                httpMethod: httpMethod,
                requestUrl: requestUrl,
                body: body);

            Assert.That(
                expectedStatusCode,
                Is.EqualTo(response.StatusCode));
        }

        private ApiClient client;
    }
}
