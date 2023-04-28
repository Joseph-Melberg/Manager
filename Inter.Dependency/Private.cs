using Inter.Common.Configuration.Providers;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Repositories;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Rabbit.Messages;
using Inter.Infrastructure.Rabbit.Publishers;
using Inter.Infrastructure.Redis.Contexts;
using Inter.Infrastructure.Redis.Repositories;
using Inter.Infrastructure.Services;
using MelbergFramework.Infrastructure.InfluxDB;
using MelbergFramework.Infrastructure.MySql;
using MelbergFramework.Infrastructure.Rabbit;
using MelbergFramework.Infrastructure.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.Dependency;
public static partial class Dependency
{
    #region Infrastructure Services


    private static IServiceCollection RegisterPlaneApiInfrastructureService(this IServiceCollection collection)        
    {
        collection.AddTransient<IPlaneApiInfrastructureService,PlaneApiInfrastructureService>();

        RedisModule.LoadRedisRepository<IPlaneCacheRepository,PlaneCacheRepository, PlaneCacheContext>(collection);

        return collection;
    }

    private static IServiceCollection RegisterNodeApiInfrastructureService(this IServiceCollection collection)
    {
        collection.AddTransient<INodeApiInfrastructureService, NodeApiInfrastructureService>();

        MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(collection);

        return collection;
    }

    #endregion Infrastructure Services

}