using Inter.Dependency;
using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.HeartbeatListenerAppService.Application;
using Inter.HeartbeatListenerAppService.Messages;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Services;
using Melberg.Infrastructure.MySql;
using Melberg.Infrastructure.Rabbit;
using Melberg.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.HeartbeatListenerAppService;
public class Register
{
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        RabbitModule.RegisterConsumer<HeartbeatProcessor>(services);
        services.AddTransient<IJsonToObjectTranslator<HeartbeatMessage>,JsonToObjectTranslator<HeartbeatMessage>>();
        services.AddTransient<IHeartbeatListenerDomainService, HeartbeatListenerDomainService>();

        services.AddTransient<IHeartbeatListenerInfrastructureService,HeartbeatListenerInfrastructureService>();
        
        MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(services);
        return services;
    }
}