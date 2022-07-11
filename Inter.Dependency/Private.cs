using Inter.Common.Configuration;
using Inter.Common.Configuration.Providers;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Rabbit.Messages;
using Inter.Infrastructure.Rabbit.Publishers;
using Inter.Infrastructure.Redis.Contexts;
using Inter.Infrastructure.Redis.Repositories;
using Inter.Infrastructure.Services;
using Melberg.Infrastructure.InfluxDB;
using Melberg.Infrastructure.MySql;
using Melberg.Infrastructure.Rabbit;
using Melberg.Infrastructure.Redis;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.Dependency;
public static partial class Dependency
{
    #region Infrastructure Services
    private static IServiceCollection RegisterPlaneIngestorInfrastructureService(this IServiceCollection collection)
    {
        collection.AddTransient<IPlaneIngestorInfrastructureService,PlaneIngestorInfrastructureService>();
        
        InfluxDBModule.LoadInfluxDBRepository<IPlaneFrameMetadataRepository,Inter.Infrastructure.InfluxDB.Repositories.PlaneFrameMetadataRepository,InfluxDBContext>(collection);
        RedisModule.LoadRedisRepository<IPlaneCacheRepository,PlaneCacheRepository, PlaneCacheContext>(collection);
        return collection;
    }

    private static IServiceCollection RegisterPlaneCongregatorInfrastructureService(this IServiceCollection collection)
    {
        collection.AddTransient<IPlaneCongregatorInfrastructureService,PlaneCongregatorInfrastructureService>();
        InfluxDBModule.LoadInfluxDBRepository<IPlaneFrameMetadataRepository,Inter.Infrastructure.InfluxDB.Repositories.PlaneFrameMetadataRepository,InfluxDBContext>(collection);

        return collection
                .RegisterPlaneCacheRepository();
    }

    private static IServiceCollection RegisterMetronomeInfrastructureService(this IServiceCollection collection)
    {
        collection.AddTransient<IMetronomeInfrastructureService,MetronomeInfrastructureService>();

        return collection
            .RegisterTickPublisher();
    }

    private static IServiceCollection RegisterLifeAlertInfrastructureService(this IServiceCollection collection)
    {
        collection.AddTransient<ILifeAlertInfrastructureService,LifeAlertInfrastructureService>();

        collection.AddSingleton<IEmailConfiguration,EmailConfigurationProvider>();

        MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(collection);

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
    #endregion Infrastructure Services

    #region Redis

    private static IServiceCollection RegisterPlaneCacheRepository(this IServiceCollection collection)
    {
        RedisModule.LoadRedisRepository<IPlaneCacheRepository,PlaneCacheRepository, PlaneCacheContext>(collection);

        return collection;
    }

    #endregion

    #region Rabbit
    private static IServiceCollection RegisterTickPublisher(this IServiceCollection collection)
    {
        collection.AddTransient<ITickPublisher,TickPublisher>();
        RabbitModule.RegisterPublisher<TickMessage>(collection);

        return collection;
    }
    #endregion Rabbit
}