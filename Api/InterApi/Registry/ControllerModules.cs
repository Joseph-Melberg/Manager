using Inter.DomainServices.Core;
using Inter.DomainServices;
using Microsoft.Extensions.DependencyInjection;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Services;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.MySQL;
using Melberg.Infrastructure.MySql;

namespace InterApi.Registry
{
    public static class ControllerModules
    {
        public static IServiceCollection RegisterNodeApi(this IServiceCollection services)
        {
            services.AddTransient<INodeStatusService, NodeStatusService>();
            services.AddTransient<INodeStatusInfrastructureService, NodeStatusInfrastructureService>();
            MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(services);
            return services;
        }
        public static IServiceCollection RegisterPlaneApi(this IServiceCollection services)
        {
            services.AddTransient<IPlaneApiService,PlaneApiService>();
            services.AddTransient<IPlaneApiInfrastructureService,PlaneApiInfrastructureService>();
            MySqlModule.LoadSqlRepository<IPlaneRepository,PlaneRepository,PlaneContext>(services);
            return services;
        }
    }
}
