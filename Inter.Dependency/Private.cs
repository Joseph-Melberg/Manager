using Inter.Common.Configuration;
using Inter.Common.Configuration.Providers;
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

namespace Inter.Dependency;
public static partial class Dependency
{
    private static IServiceCollection RegisterPlaneIngestorInfrastructureService(this IServiceCollection collection)
    {
        collection.AddTransient<IPlaneIngestorInfrastructureService,PlaneIngestorInfrastructureService>();
        
        RedisModule.LoadRedisRepository<IPlaneCacheRepository,PlaneCacheRepository, PlaneCacheContext>(collection);

        return collection;
    }
    private static IServiceCollection RegisterLifeAlertInfrastructureService(this IServiceCollection collection)
    {
        collection.AddTransient<ILifeAlertInfrastructureService,LifeAlertInfrastructureService>();

        collection.AddSingleton<IEmailConfiguration,EmailConfigurationProvider>();

        MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(collection);

        return collection;
    }
    private static IServiceCollection RegisterPlaneListenerInfrastructureService(this IServiceCollection collection)
    {
        collection.AddTransient<IPlaneListenerInfrastructureService,PlaneListenerInfrastructureService>();

        RedisModule.LoadRedisRepository<IPlaneCacheRepository,PlaneCacheRepository, PlaneCacheContext>(collection);

        MySqlModule.LoadSqlRepository<IPlaneFrameMetadataRepository,PlaneFrameMetadataRepository,ReadWriteContext>(collection);

        return collection;
    }

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