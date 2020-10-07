using System;
using System.IO;
using System.Threading.Tasks;
using Inter.HeartbeatListenerAppService.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration.FileExtensions;

namespace Inter.HeartbeatListenerAppService
{
    class Program
    {

        public static IConfigurationRoot configuration;
        private static IServiceProvider _serviceProvider;
        static async Task Main(string[] args)
        {
            RegisterServices();
            await _serviceProvider.GetRequiredService<HeartbeatProccessor>().Run();
            DisposeServices();
        }

        private static void RegisterServices()
        {

            var services = new ServiceCollection();
            Register.RegisterServices(services);
            // Build configuration
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            services.AddSingleton<IConfigurationRoot>(configuration);
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
