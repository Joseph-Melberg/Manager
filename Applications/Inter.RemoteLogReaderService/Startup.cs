using MelbergFramework.Application;
using MelbergFramework.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.RemoteLogReaderService;

public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        RabbitModule.RegisterConsumer<Processor>(services);
        Register.RegisterServices(services);
    }
}