using Microsoft.Extensions.DependencyInjection;
using Inter.PlaneListenerService.Application;
using Melberg.Infrastructure.Rabbit;
using Inter.Dependency;
using Melberg.Infrastructure.Rabbit.Translator;
using Inter.PlaneListenerService.Messages;

namespace Inter.PlaneListenerService;
public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        
        RabbitModule.RegisterConsumer<PlaneProcessor>(services);
        services.AddTransient<IJsonToObjectTranslator<PlaneMessage>,JsonToObjectTranslator<PlaneMessage>>();
        
        services.RegisterPlaneListenerService();

        return services;
    }
}