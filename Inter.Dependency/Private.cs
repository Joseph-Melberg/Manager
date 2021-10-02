using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Redis.Contexts;
using Inter.Infrastructure.Redis.Repositories;
using Inter.Infrastructure.Services;
using Melberg.Infrastructure.MySql;
using Melberg.Infrastructure.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.Dependency
{
    public static partial class Dependency
    {
        private static IServiceCollection RegisterPlaneListenerInfrastructureService(this IServiceCollection collection)
        {
            collection.AddTransient<IPlaneListenerInfrastructureService,PlaneListenerInfrastructureService>();

            RedisModule.LoadRedisRepository<IPlaneCacheRepository,PlaneCacheRepository, PlaneCacheContext>(collection);

            return collection;
        }
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

        private static IServiceCollection RegisterTemperatureListenerInfrastructureService(this IServiceCollection collection)
        {
            collection.AddTransient<ITemperatureListenerInfrastructureService,TemperatureListenerInfrastructureService>();

            MySqlModule.LoadSqlRepository<ITemperatureRepository,TemperatureRepository,TemperatureContext>(collection);

            return collection;
        }
    }
}