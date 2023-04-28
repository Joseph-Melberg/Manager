using Inter.RemoteLogReaderService.Messsage;
using MelbergFramework.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.RemoteLogReaderService;

public class Register
{
    public static IServiceCollection RegisterServices(IServiceCollection services)
    {
        
        services.AddTransient<IJsonToObjectTranslator<LogMessage>,JsonToObjectTranslator<LogMessage>>();
        return services;
    }
}