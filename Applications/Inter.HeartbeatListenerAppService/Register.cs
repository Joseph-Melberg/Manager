using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.HeartbeatListenerAppService.Application;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Services;
using Melberg.Infrastructure.MySql;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.HeartbeatListenerAppService
{
    public class Register
    {
        public static ServiceCollection RegisterServices(ServiceCollection services)
        {
            services.AddSingleton<HeartbeatProccessor>();
            services.AddTransient<IHeartbeatListenerService, HeartbeatListenerService>();
            services.AddTransient<IHeartbeatListenerInfrastructureService,HeartbeatListenerInfrastructureService>();
            MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(services);
            return services;
        }
    }
}
