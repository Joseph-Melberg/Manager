using Inter.DomainServices.Core;
using Inter.DomainServices;
using Microsoft.Extensions.DependencyInjection;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Services;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Repositories;
using Melberg.Infrastructure.MySql;

namespace InterApi.Registry
{
    public static class ControllerModules
    {
        public static IServiceCollection RegisterNodeApi(this IServiceCollection services)
        {
            services.AddTransient<INodeApiInfrastructureService, NodeApiInfrastructureService>();
            services.AddTransient<INodeApiInfrastructureService, NodeApiInfrastructureService>();
            MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(services);
            return services;
        }
    }
}
