using Inter.Dependency;
using Inter.HeartbeatListenerAppService.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.HeartbeatListenerAppService
{
    public class Register
    {
        public static ServiceCollection RegisterServices(ServiceCollection services)
        {
            services.AddSingleton<HeartbeatProccessor>();
            services.RegisterHeartbeatListenerService();
            return services;
        }
    }
}
