using Inter.Dependency;
using Inter.HeartbeatListenerAppService.Application;
using Inter.HeartbeatListenerAppService.Messages;
using Melberg.Infrastructure.Rabbit;
using Melberg.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.HeartbeatListenerAppService;
public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        RabbitModule.RegisterConsumer<HeartbeatProcessor>(services);
        services.AddTransient<IJsonToObjectTranslator<HeartbeatMessage>,JsonToObjectTranslator<HeartbeatMessage>>();
        services.RegisterHeartbeatListenerService();
        return services;
    }
}