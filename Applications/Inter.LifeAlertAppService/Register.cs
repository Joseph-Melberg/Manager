using System;
using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Services;
using Melberg.Infrastructure.MySql;
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
            MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(services);

            return services;
        }
    }
}
