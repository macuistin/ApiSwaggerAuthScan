using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiSwaggerAuth.Domain.Entities;
using ApiSwaggerAuth.Domain.Extensions;

namespace ApiSwaggerAuth.Domain.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISwaggerService _swaggerService;

        public ApiService(IHttpClientFactory httpClientFactory, ISwaggerService swaggerService)
        {
            _httpClientFactory = httpClientFactory;
            _swaggerService = swaggerService;
        }

        public async Task<ApiData> GetAsync(string apiUrl)
        {
            var data = new ApiData(apiUrl);
            if (ValidateUrl(apiUrl))
            {
                // Can replace synchronous calls with events to update object and pass though to UI
                data.Status = ApiStatus.Valid;
                var success = await ProbeAsync(data);
                data.SwaggerResults = success ? _swaggerService.ScanAsync(data.ApiUrl) : null;

            }
            else
            {
                data.Status = ApiStatus.InvalidUrl;
            }

            return data;
        }

        private async Task<bool> ProbeAsync(ApiData data)
        {
            using var httpClient = _httpClientFactory.CreateClient("probeClient");
            data.ProbeStatus = await httpClient.ProbeAsync(data.ApiUrl);

            return data.ProbeStatus == System.Net.HttpStatusCode.OK;
        }

        private static bool ValidateUrl(string apiUrl)
        {
            return !string.IsNullOrEmpty(apiUrl) && Uri.IsWellFormedUriString(apiUrl, UriKind.Absolute);
        }

    }
}
