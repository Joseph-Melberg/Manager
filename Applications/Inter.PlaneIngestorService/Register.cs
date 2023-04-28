using Microsoft.Extensions.DependencyInjection;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Services;
using Inter.Infrastructure.Corral;
using MelbergFramework.Infrastructure.InfluxDB;
using MelbergFramework.Infrastructure.Redis;
using Inter.Infrastructure.Redis.Repositories;
using Inter.Infrastructure.Redis.Contexts;
using Inter.Infrastructure.InfluxDB.Contexts;

namespace Inter.PlaneIngestorService;

public class Register
{
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        
        services.AddTransient<IPlaneIngestorDomainService,DomainServices.PlaneIngestorDomainService>();

        services.AddTransient<IPlaneIngestorInfrastructureService,PlaneIngestorInfrastructureService>();
        
        InfluxDBModule.LoadInfluxDBRepository<IPlaneFrameMetadataRepository,Inter.Infrastructure.InfluxDB.Repositories.PlaneFrameMetadataRepository,InfluxDBContext>(services);
        RedisModule.LoadRedisRepository<IPlaneCacheRepository,PlaneCacheRepository, PlaneCacheContext>(services);
        return services;
    }
}