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
        services.AddTransient<IMetronomeDomainService,MetronomeDomainService>();
        services.AddTransient<IClock,Clock>();
        services.AddTransient<IMetronomeInfrastructureService,MetronomeInfrastructureService>();
        services.AddTransient<IMinutePublisher,MinutePublisher>();
        RabbitModule.RegisterPublisher<MinuteMessage>(services);
        services.AddTransient<ITickPublisher,TickPublisher>();
        RabbitModule.RegisterPublisher<TickMessage>(services);
        return services;
    }
}