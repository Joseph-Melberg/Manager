using Microsoft.Extensions.DependencyInjection;
using Inter.PlaneListenerAppService.Application;
using Inter.Infrastructure.Core;
using Inter.DomainServices.Core;
using Inter.DomainServices;
using Inter.Infrastructure.Services;

namespace Inter.PlaneListenerAppService
{
    public static class Register
    {
        public static ServiceCollection RegisterServices(ServiceCollection services)
        {
            services.AddSingleton<PlaneProcessor>();
            services.AddTransient<IPlaneListenerService,PlaneListenerService>();
            services.AddTransient<IPlaneListenerInfrastructureService,PlaneListenerInfrastructureService>();
            return services;
        }
    }
}