using System;
using Inter.Dependency;
using Microsoft.Extensions.DependencyInjection;
namespace Inter.LifeAlertAppService;
public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        services.RegisterLifeAlertService();

        return services;
    }
}