using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Services;
using Melberg.Infrastructure.MySql;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.Dependency
{
    public static partial class Dependency
    {
        private static IServiceCollection RegisterNodeApiInfrastructureService(this IServiceCollection Collection)
        {
            Collection.AddTransient<INodeApiInfrastructureService, NodeApiInfrastructureService>();
            
            //MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(Collection);

            return Collection;
        }

        private static IServiceCollection RegisterHearbeatListenerInfrastructureService(this IServiceCollection Collection)
        {
            Collection.AddTransient<IHeartbeatListenerInfrastructureService,HeartbeatListenerInfrastructureService>();
            MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(Collection);
            
            return Collection;
        }
    }
}