using Inter.Dependency;
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
using Melberg.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.LogRecieverAppService
{
    public class Register
    {
        public static ServiceCollection RegisterServices(ServiceCollection services)
        {
            RabbitModule.RegisterConsumer<LogProcessor>(services);
            services.RegisterLogListenerService();
            return services;
        }
    }
}
