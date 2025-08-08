using NUnitDemo.Constants;
using NUnitDemo.Utilities;
using System.Collections;
using System.Net;

namespace NUnitDemo.Fixtures.ApiFixtures.Scenarios
{
    public static class ApiRequestScenarios
    {
        public static IEnumerable Positive_RequestScenarios()
        {
            yield return new TestCaseData(
                    HttpMethod.Get,
                    "https://jsonplaceholder.typicode.com/todos/1",
                    null,
                    HttpStatusCode.OK)
                .SetName("Api GET: Test Get");
            yield return new TestCaseData(
                    HttpMethod.Post,
                    "https://jsonplaceholder.typicode.com/posts",
                    new 
                    {
                        body = StringUtilities.GenerateRandomStrin(),
                        count = StringUtilities.GenerateRandomStrin()
                    },
                    HttpStatusCode.Created)
                .SetName("Api POST: Test Create");
            yield return new TestCaseData(
                    HttpMethod.Delete,
                    "https://jsonplaceholder.typicode.com/posts/1",
                    null,
                    HttpStatusCode.OK)
                .SetName("Api DELETE: Test Delete");
        }

        public static IEnumerable Negative_RequestScenarios()
        {
            yield return new TestCaseData(
                    HttpMethod.Post,
                    "https://jsonplaceholder.typicode.com/todos/1",
                    null,
                    HttpStatusCode.NotFound)
                .SetName("Api GET: Call GET endpoint with POST method");
        }
    }
}
