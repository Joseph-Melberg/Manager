using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Services;
using Inter.LogRecieverAppService.Application;
using Microsoft.Extensions.DependencyInjection;
namespace Inter.LogRecieverAppService
{
    public class Register
    {
        public static ServiceCollection RegisterServices(ServiceCollection services)
        {
            services.AddSingleton<LogProcessor>();
            services.AddTransient<ILogListenerService, LogListenerService>();
            services.AddTransient<ILogListenerInfrastructureService,
                LogListenerInfrastructureService>();
            services.AddScoped<ILogRepository, LogRepository>();
            return services;
        }
    }
}
