using Inter.Common.Clock;
using Inter.Dependency;
using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.Rabbit.Messages;
using Inter.Infrastructure.Rabbit.Publishers;
using Inter.Infrastructure.Services;
using Melberg.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.MetronomeService;

public class Register
{
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        return services;
    }
}