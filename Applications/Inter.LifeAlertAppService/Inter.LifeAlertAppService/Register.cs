using System;
using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.LifeAlertAppService
{
    public class Register
    {
        public static ServiceCollection RegisterServices(ServiceCollection services)
        {
            services.AddTransient<ILifeAlertService, LifeAlertService>();
            services.AddTransient<ILifeAlertInfrastructureService,
                LifeAlertInfrastructureService>();
            return services;
        }
    }
}
