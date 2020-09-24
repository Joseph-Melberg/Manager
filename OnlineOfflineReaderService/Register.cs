using System;
using Microsoft.Extensions.DependencyInjection;
using OnlineOfflineReaderService.DomainService;
using OnlineOfflineReaderService.DomainService.Core;
using OnlineOfflineReaderService.Infrastructure;
using OnlineOfflineReaderService.Infrastructure.Core;
using OnlineOfflineReaderService.Infrastructure.MySql;

namespace OnlineOfflineReaderService
{
    public class Register
    {
        public static ServiceCollection RegisterServices(ServiceCollection services)
        {
            services.AddTransient<IHeartBeatService, HeartBeatService>();
            services.AddTransient<IHeartBeatInfrastructureService,
                HeartBeatInfrastructureService>();
            services.AddTransient<IHeartBeatContext, HeartBeatContext>();
            services.AddTransient<IHeartBeatRepository, HeartBeatRepository>();
            return services;
        }
    }
}
