using Inter.Dependency;
using Inter.TempLoggerAppService.Application;
using Melberg.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.TempLoggerAppService;
public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        RabbitModule.RegisterConsumer<TemperatureProcessor>(services);
        
        services.RegisterTemperatureListenerService();
       
        return services;
    }
}