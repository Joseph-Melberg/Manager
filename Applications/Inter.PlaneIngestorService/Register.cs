using Melberg.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;
using Inter.Dependency;

namespace Inter.PlaneIngestorService;

public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        
        RabbitModule.RegisterConsumer<PlaneProcessor>(services);
        
        services.RegisterPlaneIngestorService(services);

        return services;
    }
}