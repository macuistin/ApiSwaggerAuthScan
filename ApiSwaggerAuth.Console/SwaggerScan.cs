using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using ApiSwaggerAuth.Domain.Entities;
using ApiSwaggerAuth.Domain.Services;

namespace ApiSwaggerAuth.Console
{
    public class SwaggerScan : ISwaggerScan
    {
        private readonly IApiService _apiService;

        public SwaggerScan(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task ScanAndOutputResponse(string url)
        {
            var stopwatch = Stopwatch.StartNew();
            LogInfo($"Scanning {url}");

            var data = await _apiService.GetAsync(url);

            if (data == null)
            {
                LogInfo("... What happened?");
            }

            await OutputDataAsync(data);

            stopwatch.Stop();

            LogInfo($"*** Completed in {stopwatch.Elapsed:g} ***\n");
        }

        private static async Task OutputDataAsync(ApiData data)
        {
            LogInfo($"Current Status: {data.Status}\tProbe Status: {data.ProbeStatus}");

            if (data.ProbeStatus == HttpStatusCode.OK)
            {
                LogInfo("Swagger Docs:");
                await foreach (var swaggerResult in data.SwaggerResults)
                {
                    LogInfo($"\t{swaggerResult.Url} : [{swaggerResult.Name}]\n\t\tOperations:");
                    await foreach (var methodResult in swaggerResult.MethodProbeResults)
                    {
                        var message = $"\t\t{methodResult.StatusCode} :\t[{methodResult.Verb}]\t{methodResult.Path}";
                        switch (methodResult.StatusCode)
                        {
                            case HttpStatusCode.Unauthorized:
                                LogInfo(message);
                                break;
                            case HttpStatusCode.OK:
                                LogError(message);
                                break;
                            default:
                                LogWarning(message);
                                break;
                        }
                    }
                }
            }

        }

        private static void LogInfo(string message)
        {
            System.Console.BackgroundColor = ConsoleColor.Black;
            System.Console.ForegroundColor = ConsoleColor.White;
            System.Console.WriteLine($"[INFO]  {message}");
        }

        private static void LogWarning(string message)
        {
            System.Console.BackgroundColor = ConsoleColor.Black;
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine($"[WARN]  {message}");
        }

        private static void LogError(string message)
        {
            System.Console.BackgroundColor = ConsoleColor.Black;
            System.Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"[ERR]   {message}");
        }
    }
}
