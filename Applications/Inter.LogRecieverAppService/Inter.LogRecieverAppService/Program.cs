using System;
using System.Threading.Tasks;
using Inter.LogRecieverAppService.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.LogRecieverAppService
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        static async Task Main(string[] args)
        {
            RegisterServices();
            await _serviceProvider.GetRequiredService<LogProcessor>().Run();
            DisposeServices();
        }

        private static void RegisterServices()
        {

            var services = new ServiceCollection();
            Register.RegisterServices(services);
            // Build configuration
            //configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            //    .AddJsonFile("appsettings.json", false)
            //    .Build();

            //services.AddSingleton<IConfigurationRoot>(configuration);
            // Add access to generic IConfigurationRoot
            _serviceProvider = services.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}