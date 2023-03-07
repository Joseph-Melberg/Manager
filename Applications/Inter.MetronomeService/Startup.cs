using Inter.Common.Clock;
using Inter.DomainServices;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.Rabbit.Messages;
using Inter.Infrastructure.Rabbit.Publishers;
using Melberg.Application;
using Melberg.Infrastructure.Rabbit;
using Inter.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.MetronomeService;

public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IMetronomeDomainService,MetronomeDomainService>();
        services.AddTransient<IClock,Clock>();
        services.AddTransient<IMetronomeInfrastructureService,MetronomeInfrastructureService>();
        services.AddTransient<IMinutePublisher,MinutePublisher>();
        services.AddTransient<ITickPublisher,TickPublisher>();
        services.AddHostedService<MetronomeApplicationService>();
        RabbitModule.RegisterPublisher<MinuteMessage>(services);
        RabbitModule.RegisterPublisher<TickMessage>(services);
    }
}