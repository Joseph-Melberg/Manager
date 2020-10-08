using System;
using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
namespace Inter.LifeAlertAppService
{
    public class Register
    {
        public static ServiceCollection RegisterServices(ServiceCollection services)
        {
            services.AddTransient<ILifeAlertService, LifeAlertService>();
            services.AddTransient<ILifeAlertInfrastructureService,
                LifeAlertInfrastructureService>();
            services.AddTransient<IHeartbeatContext, HeartbeatContext>();
            services.AddTransient<IHeartbeatRepository, HeartbeatRepository>();
            return services;
        }
    }
}
