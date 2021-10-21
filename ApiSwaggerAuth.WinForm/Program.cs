using System;
using System.Windows.Forms;
using ApiSwaggerAuth.Domain.Configuration;
using ApiSwaggerAuth.Domain.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiSwaggerAuth.WinForm
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var serviceProvider = LoadServices();
            var form = serviceProvider.GetRequiredService<ApiScanForm>();
            
            Application.Run(form);
        }

        private static ServiceProvider LoadServices()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json",true,false)
                .Build();

            var swaggerScannSettings = new SwaggerScanSettings();
            config.Bind("SwaggerScan", swaggerScannSettings);

            var services = new ServiceCollection();
            services.AddDomainDependencies();
            services.AddScoped<ApiScanForm>();
            services.AddSingleton(swaggerScannSettings);
            return services.BuildServiceProvider();
        }
    }
}
