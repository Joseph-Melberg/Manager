using Melberg.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;
using Inter.PlaneIngestorService.Application;
using Inter.Dependency;

namespace Inter.PlaneIngestorService;

public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        
        RabbitModule.RegisterConsumer<Processor>(services);
        
        services.RegisterPlaneIngestorService();

        return services;
    }
}