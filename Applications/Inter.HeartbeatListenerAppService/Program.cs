using System;
using System.Threading.Tasks;
using Inter.HeartbeatListenerAppService.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.HeartbeatListenerAppService
{
    class Program
    {
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
            /*services.AddSingleton<HearteatProccessor>();
            services.AddTransient<IHeartBeatService, HeartBeatService>();
            services.AddTransient<IHeartBeatInfrastructureService,
                HeartBeatInfrastructureService>();
            services.AddTransient<IHeartBeatContext, HeartBeatContext>();
            services.AddTransient<IHeartBeatRepository, HeartBeatRepository>();
            */
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
