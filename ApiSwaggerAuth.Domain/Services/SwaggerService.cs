using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ApiSwaggerAuth.Domain.Configuration;
using ApiSwaggerAuth.Domain.Entities;
using ApiSwaggerAuth.Domain.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;

namespace ApiSwaggerAuth.Domain.Services
{
    public class SwaggerService : ISwaggerService
    {
        private readonly SwaggerScanSettings _settings;
        private readonly IHttpClientFactory _httpClientFactory;
        public OpenApiDiagnostic Diagnostic { get; private set; }

        public SwaggerService(
            SwaggerScanSettings settings, 
            IHttpClientFactory httpClientFactory)
        {
            _settings = settings;
            _httpClientFactory = httpClientFactory;
        }

        private async Task<OpenApiDocument> GetOpenApiDocumentAsync(HttpClient httpClient, string swaggerUrl)
        {
            try
            {
                return await ReadDocument(httpClient, swaggerUrl);
            }
            catch
            {
                return null;
            }

        }

        private async Task<OpenApiDocument> ReadDocument(HttpClient httpClient, string swaggerUrl)
        {
            var stream = await httpClient.GetStreamAsync(swaggerUrl);
            var document = new OpenApiStreamReader().Read(stream, out var diagnostic);

            Diagnostic = diagnostic;
            return document;
        }

        public async IAsyncEnumerable<SwaggerScanResult> ScanAsync(Uri apiUrl)
        {
            if (_settings?.RelativePaths == null || _settings.RelativePaths.Length == 0)
            {
                yield break;
            }

            using var httpClient = _httpClientFactory.CreateClient("probeClient");
            httpClient.BaseAddress = apiUrl;
            var paths = await GetSwaggerPathsAsync(httpClient);

            foreach (var path in paths)
            {
                var scanResult = await ScanRelativeAsync(httpClient, path);
                if (scanResult == null)
                {
                    path.Url = $"swagger/{path.Url}";
                    scanResult = await ScanRelativeAsync(httpClient, path);
                }
                if (scanResult != null)
                    yield return scanResult;
            }
        }

        public async Task<IEnumerable<SwaggerUrlEntry>> GetSwaggerPathsAsync(HttpClient httpClient)
        {
            var paths = await GetSwaggerPathsFromPageAsync(httpClient);

            return paths ?? _settings.RelativePaths.Select(p => new SwaggerUrlEntry { Url = p });
        }

        private static async Task<IEnumerable<SwaggerUrlEntry>> GetSwaggerPathsFromPageAsync(HttpClient httpClient)
        {
            try
            {
                var indexPage = await httpClient.GetStringAsync("swagger/index.html");

                var match = Regex.Match(indexPage, @"^\s*var\sconfigObject\s=\sJSON\.parse\('(?'swaggersettings'{.*$)", RegexOptions.Multiline);
                if (match.Success)
                {

                    var swaggerPageConfig = match.Groups[1].Value.Replace("');", "");
                    var swaggerPageConfigDoc = System.Text.Json.JsonDocument.Parse(swaggerPageConfig);
                    var urlsEntry = swaggerPageConfigDoc.RootElement.GetProperty("urls");
                    return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<SwaggerUrlEntry>>(urlsEntry.ToString());
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private async Task<SwaggerScanResult> ScanRelativeAsync(HttpClient httpClient, SwaggerUrlEntry relativePath)
        {
            var swaggerDoc = await GetOpenApiDocumentAsync(httpClient, relativePath.Url);
            if (swaggerDoc == null)
            {
                return null;
            }

            var methodProbeResults = ProbeMethodsAsync(httpClient, swaggerDoc);

            return new SwaggerScanResult 
            { 
                Url = relativePath.Url,
                Name = relativePath.Name,
                MethodProbeResults = methodProbeResults
            };

        }

        private async IAsyncEnumerable<SwaggerProbeResult> ProbeMethodsAsync(HttpClient httpClient, OpenApiDocument swaggerDoc)
        {
            foreach (var path in swaggerDoc.Paths)
            {
                foreach (var operation in path.Value.Operations)
                {
                    var response = await httpClient.CallBasicMethodAsync(path.Key, operation.Key.ToHttpMethod());

                    yield return new SwaggerProbeResult
                    {
                        Path = path.Key,
                        StatusCode = response.StatusCode,
                        Verb = operation.Key.ToString()
                    };
                }
            }
        }
    }
}
