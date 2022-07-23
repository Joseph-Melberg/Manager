using System;
using Inter.Dependency;
using Inter.LifeAlertAppService.Application;
using Melberg.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;
namespace Inter.LifeAlertAppService;
public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        RabbitModule.RegisterConsumer<Processor>(services);
        services.RegisterLifeAlertService();

        return services;
    }
}