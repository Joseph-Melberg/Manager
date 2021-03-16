using Microsoft.Extensions.DependencyInjection;

namespace Inter.PlaneListenerService
{
    public class Register
    {
        public static ServiceCollection RegisterServices(ServiceCollection services)
        {
            services.AddTransient<IPlaneListenerService, LifeAlertService>();
            services.AddTransient<ILifeAlertInfrastructureService,
                LifeAlertInfrastructureService>();
            MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(services);

            return services;
        }
    }
}

    