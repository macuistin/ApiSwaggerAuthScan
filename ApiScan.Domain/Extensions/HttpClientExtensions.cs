using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiSwaggerAuth.Domain.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<HttpStatusCode> ProbeAsync(this HttpClient httpClient, Uri baseUrl)
        {
            try
            {
                var probeUrl = new Uri(baseUrl, "probe");
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));
                var probeResult = await httpClient.GetAsync(probeUrl, cts.Token);

                return probeResult.StatusCode;
            }
            catch (HttpRequestException)
            {
                return HttpStatusCode.BadGateway;
            }
            catch (TaskCanceledException)
            {
                return HttpStatusCode.RequestTimeout;
            }
        }

        public static async Task<HttpResponseMessage> CallBasicMethodAsync(this HttpClient httpClient, string relativePath, HttpMethod method)
        {
            var request = new HttpRequestMessage(method, relativePath);
            return await httpClient.SendAsync(request);
        }
    }
}
