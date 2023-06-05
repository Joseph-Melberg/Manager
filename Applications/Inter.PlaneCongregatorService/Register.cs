using Inter.PlaneCongregatorService.Application;
using MelbergFramework.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;
using MelbergFramework.Infrastructure.Rabbit.Translator;
using Inter.Infrastructure.Rabbit.Messages;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Services;
using Inter.Infrastructure.Corral;
using MelbergFramework.Infrastructure.InfluxDB;
using Inter.Infrastructure.InfluxDB.Contexts;
using MelbergFramework.Infrastructure.Redis;
using Inter.Infrastructure.Redis.Repositories;
using Inter.Infrastructure.Redis.Contexts;

namespace Inter.PlaneCongregatorService;

class Register
{
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        
        RabbitModule.RegisterConsumerWithMetrics<Processor>(services);
        services.AddTransient<IJsonToObjectTranslator<TickMessage>,JsonToObjectTranslator<TickMessage>>();
        services.AddTransient<IPlaneCongregatorDomainService,Inter.DomainServices.PlaneCongregatorDomainService>();
        services.AddTransient<IPlaneCongregatorInfrastructureService,PlaneCongregatorInfrastructureService>();
        InfluxDBModule.LoadInfluxDBRepository<IPlaneFrameMetadataRepository, Infrastructure.InfluxDB.Repositories.PlaneFrameMetadataRepository, InfluxDBContext>(services);
        RedisModule.LoadRedisRepository<IPlaneCacheRepository,PlaneCacheRepository, PlaneCacheContext>(services);
        return services;
    }
}