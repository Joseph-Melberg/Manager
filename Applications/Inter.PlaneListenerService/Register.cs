using Microsoft.Extensions.DependencyInjection;
using Inter.PlaneListenerService.Application;
using Melberg.Infrastructure.Rabbit;
using Inter.Dependency;

namespace Inter.PlaneListenerService
{
    public class Register
    {
        public static ServiceCollection RegisterServices(ServiceCollection services)
        {
            
            RabbitModule.RegisterConsumer<PlaneProcessor>(services);
            
            services.RegisterPlaneListenerService();

            return services;
        }
    }
}

    