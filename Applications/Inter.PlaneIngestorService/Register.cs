using Melberg.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;
using Inter.PlaneIngestorService.Application;
using Inter.Dependency;
using Melberg.Infrastructure.Rabbit.Translator;
using Inter.PlaneIngestorService.Messages;

namespace Inter.PlaneIngestorService;

public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        
        RabbitModule.RegisterConsumer<Processor>(services);
        services.AddTransient<IJsonToObjectTranslator<PlaneIngestionMessage>,JsonToObjectTranslator<PlaneIngestionMessage>>();
        services.RegisterPlaneIngestorService();

        return services;
    }
}