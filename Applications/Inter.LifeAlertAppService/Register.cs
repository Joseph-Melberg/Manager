using System;
using Inter.Common.Configuration;
using Inter.Common.Configuration.Providers;
using Inter.Dependency;
using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.InfluxDB.Contexts;
using Inter.Infrastructure.InfluxDB.Repositories;
using Inter.Infrastructure.MySQL.Contexts;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.Services;
using Inter.LifeAlertAppService.Application;
using MelbergFramework.Infrastructure.InfluxDB;
using MelbergFramework.Infrastructure.MySql;
using MelbergFramework.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;
namespace Inter.LifeAlertAppService;
public class Register
{
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        RabbitModule.RegisterConsumer<Processor>(services);

        services.AddTransient<ILifeAlertDomainService,LifeAlertDomainService>();
        services.AddSingleton<ILifeAlertRateConfiguration,LifeAlertRateConfigurationProvider>();
        services.AddSingleton<IEmailRecipientConfiguration,EmailRecipientConfigurationProvider>();

        services.AddTransient<ILifeAlertInfrastructureService,LifeAlertInfrastructureService>();

        services.AddSingleton<IEmailConfiguration,EmailConfigurationProvider>();

        MySqlModule.LoadSqlRepository<IHeartbeatRepository, HeartbeatRepository, HeartbeatContext>(services);
        InfluxDBModule.LoadInfluxDBRepository<INodeStateMarkRepository,NodeStateMarkRepository,InfluxDBContext>(services);

        return services;
    }
}