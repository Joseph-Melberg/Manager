using Inter.Dependency;
using Inter.TempLoggerAppService.Application;
using Inter.TempLoggerAppService.Messages;
using Melberg.Infrastructure.Rabbit;
using Melberg.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.TempLoggerAppService;
public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        RabbitModule.RegisterConsumer<TemperatureProcessor>(services);
        services.AddTransient<IJsonToObjectTranslator<TemperatureMessage>,JsonToObjectTranslator<TemperatureMessage>>();
        
        services.RegisterTemperatureListenerService();
       
        return services;
    }
}