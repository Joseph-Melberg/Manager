using Inter.Dependency;
using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Repositories;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Services;
using Inter.TempLoggerAppService.Application;
using Inter.TempLoggerAppService.Messages;
using Melberg.Infrastructure.InfluxDB;
using Melberg.Infrastructure.Rabbit;
using Melberg.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.TempLoggerAppService;
public class Register
{
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        RabbitModule.RegisterConsumer<TemperatureProcessor>(services);
        services.AddTransient<IJsonToObjectTranslator<TemperatureMessage>,JsonToObjectTranslator<TemperatureMessage>>();
        
        services.AddTransient<ITemperatureListenerDomainService,TemperatureListenerDomainService>();

        services.AddTransient<ITemperatureListenerInfrastructureService,TemperatureListenerInfrastructureService>();

        InfluxDBModule.LoadInfluxDBRepository<ITemperatureMarkRepository,TemperatureMarkRepository,InfluxDBContext>(services);
        return services;
    }
}