using Inter.PlaneIngestorService.Application;
using Inter.PlaneIngestorService.Messages;
using Melberg.Application;
using Melberg.Infrastructure.Rabbit;
using Melberg.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.PlaneIngestorService;

public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        RabbitModule.RegisterConsumer<Processor>(services);
        services.AddTransient<IJsonToObjectTranslator<PlaneIngestionMessage>,JsonToObjectTranslator<PlaneIngestionMessage>>();
        Register.RegisterServices(services);
    }
}