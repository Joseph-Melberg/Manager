using Inter.Dependency;
using Inter.HeartbeatListenerAppService.Application;
using Melberg.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.HeartbeatListenerAppService;
public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        RabbitModule.RegisterConsumer<HeartbeatProcessor>(services);
        services.RegisterHeartbeatListenerService();
        return services;
    }
}