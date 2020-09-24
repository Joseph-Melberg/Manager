using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OnlineOfflineReaderService.DomainService.Core;
using OnlineOfflineReaderService.DomainService;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using OnlineOfflineReaderService.Infrastructure.Core;
using OnlineOfflineReaderService.Infrastructure;
using OnlineOfflineReaderService.Infrastructure.MySql;
using OnlineOfflineReaderService.Processors;
using OnlineOfflineReaderService.Domain;

namespace OnlineOfflineReaderService
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        static async Task Main(string[] args)
        {
            RegisterServices();
            await _serviceProvider.GetRequiredService<HeartBeatProccessor>().Run();
            DisposeServices();
        }

        private static void RegisterServices()
        {

            var services = new ServiceCollection();
            services.AddSingleton<HeartBeatProccessor>();
            services.AddTransient<IHeartBeatService, HeartBeatService>();
            services.AddTransient<IHeartBeatInfrastructureService,
                HeartBeatInfrastructureService>();
            services.AddTransient<IHeartBeatContext, HeartBeatContext>();
            services.AddTransient<IHeartBeatRepository, HeartBeatRepository>();
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
