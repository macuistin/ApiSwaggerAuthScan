using ApiSwaggerAuth.Domain.Configuration;
using ApiSwaggerAuth.Domain.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace ApiSwaggerAuth.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = LoadServices();
            var swaggerScanner = serviceProvider.GetRequiredService<ISwaggerScan>();

            swaggerScanner.ScanAndOutputResponse(args[0]).GetAwaiter().GetResult();
        }


        private static ServiceProvider LoadServices()
        {
            var swaggerScanSettings = new SwaggerScanSettings
            {
                RelativePaths = new[]
                {
                    "/swagger/v1/swagger.json",
                    "/swagger/v1.0/swagger.json",
                    "/swagger/v1.1/swagger.json",
                    "/swagger/v2/swagger.json",
                    "/swagger/v2.0/swagger.json",
                    "/swagger/v3/swagger.json",
                    "/swagger/v4/swagger.json",
                    "/swagger/v3.0/swagger.json",
                    "/swagger/v4.0/swagger.json",
                    "/swagger/1.0/swagger.json",
                    "/swagger/2.0/swagger.json",
                    "/swagger/3.0/swagger.json",
                    "/swagger/4.0/swagger.json"
                }
            };

            var services = new ServiceCollection();
            services.AddDomainDependencies();
            services.AddSingleton(swaggerScanSettings);
            services.AddScoped<ISwaggerScan, SwaggerScan>();
            return services.BuildServiceProvider();
        }
    }
}
