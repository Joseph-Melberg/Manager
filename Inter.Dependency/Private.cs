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
        private static IServiceCollection RegisterNodeApiInfrastructureService(this IServiceCollection collection)
        {
            collection.AddTransient<INodeApiInfrastructureService, NodeApiInfrastructureService>();

            MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(collection);

            return collection;
        }

        private static IServiceCollection RegisterHeartbeatListenerInfrastructureService(this IServiceCollection collection)
        {
            collection.AddTransient<IHeartbeatListenerInfrastructureService,HeartbeatListenerInfrastructureService>();
            
            MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(collection);

            return collection;
        }

        private static IServiceCollection RegisterLogListenerInfrastructureService(this IServiceCollection collection)
        {
            collection.AddTransient<ILogListenerInfrastructureService,LogListenerInfrastructureService>();
            MySqlModule.LoadSqlRepository<ILogRepository, LogRepository, LogContext>(collection);

            return collection;
        }
    }
}