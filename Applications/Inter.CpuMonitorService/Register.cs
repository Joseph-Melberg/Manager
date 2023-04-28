using Inter.CpuMonitorService.Messages;
using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Repositories;
using Inter.Infrastructure.Services;
using MelbergFramework.Infrastructure.InfluxDB;
using MelbergFramework.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.CpuMonitorService;

public class Register
{
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        
        services.AddTransient<IJsonToObjectTranslator<CpuUsageMessage>,JsonToObjectTranslator<CpuUsageMessage>>();
        services.AddTransient<ICpuMonitorDomainService,CpuMonitorDomainService>();

        services.AddTransient<ICpuMonitorInfrastructureService,CpuMonitorInfrastructureService>();
        InfluxDBModule.LoadInfluxDBRepository<ICpuUtilizationMarkRepository,CpuUtilizationMarkRepository,InfluxDBContext>(services);
        return services;
    }
}