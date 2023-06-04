using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Repositories;
using Inter.Infrastructure.Services;
using Inter.MetricMonitorAppService.Application;
using MelbergFramework.Infrastructure.InfluxDB;
using MelbergFramework.Infrastructure.Rabbit;
using MelbergFramework.Infrastructure.Rabbit.Metrics;
using MelbergFramework.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.MetricMonitorAppService;

public static class Register
{
    public static void RegisterServices(IServiceCollection catalog)
    {
        RabbitModule.RegisterConsumer<Processor>(catalog);
        catalog.AddTransient<IJsonToObjectTranslator<MetricMessage>,JsonToObjectTranslator<MetricMessage>>();
        InfluxDBModule.LoadInfluxDBRepository<
            IMetricMarkRepository,
            MetricMarkRepository,
            InfluxDBContext>(catalog);
        catalog.AddTransient<IMetricMonitorDomainService, MetricMonitorDomainService>();
        catalog.AddTransient<IMetricMonitorInfrastructureService, MetricMonitorInfrastructureService>();
    }
}