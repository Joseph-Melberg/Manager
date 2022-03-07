using Inter.PlaneCongregatorService.Application;
using Melberg.Infrastructure.Rabbit;
using Inter.Dependency;
using Microsoft.Extensions.DependencyInjection;
using Melberg.Infrastructure.Rabbit.Translator;
using Inter.Infrastructure.Rabbit.Messages;

namespace Inter.PlaneCongregatorService;

public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        
        RabbitModule.RegisterConsumer<Processor>(services);
        services.AddTransient<IJsonToObjectTranslator<TickMessage>,JsonToObjectTranslator<TickMessage>>();
        services.RegisterPlaneCongregatorService();
        return services;
    }
}