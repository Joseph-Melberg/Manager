using Inter.PlaneIngestorService.Application;
using Inter.PlaneIngestorService.Messages;
using MelbergFramework.Application;
using MelbergFramework.Infrastructure.Rabbit;
using MelbergFramework.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.PlaneIngestorService;

public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        RabbitModule.RegisterConsumerWithMetrics<Processor>(services);
        services.AddTransient<IJsonToObjectTranslator<PlaneIngestionMessage>,JsonToObjectTranslator<PlaneIngestionMessage>>();
        Register.RegisterServices(services);
    }
}