using ApiSwaggerAuth.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApiSwaggerAuth.Domain.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDomainDependencies(this IServiceCollection services)
        {
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<ISwaggerService, SwaggerService>();
            services.AddHttpClient();
        }
    }
}
