using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Services;
using Inter.LogRecieverAppService.Application;
using Melberg.Infrastructure.MySql;
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
            MySqlModule.LoadSqlRepository<ILogRepository, LogRepository, LogContext>(services);
            return services;
        }
    }
}
