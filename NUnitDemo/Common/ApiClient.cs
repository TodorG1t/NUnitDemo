using System.Text;
using System.Text.Json;

namespace NUnitDemo.Common
{
    public class ApiClient
    {
        public async Task<HttpResponseMessage> SendHttpRequestAsync(
            HttpMethod httpMethod,
            string requestUrl,
            Dictionary<string, string>? requestHeaders = null,
            object? body = null)
        {
            using var httpClient = new HttpClient();
            var request = new HttpRequestMessage(httpMethod, requestUrl);

            if (body != null)
            {
                var jsonBody = JsonSerializer.Serialize(body);
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            }

            httpClient.DefaultRequestHeaders.Clear();

            if (requestHeaders != null)
            {
                foreach (var header in requestHeaders)
                {
                    httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            TestContext.WriteLine($"Send request to {requestUrl} with method '{httpMethod}' and body '{body ?? "empty body"}'");

            var response = await httpClient.SendAsync(request);

            TestContext.WriteLine($"Request response has status code '{response.StatusCode}'");

            return response;
        }
    }
}
