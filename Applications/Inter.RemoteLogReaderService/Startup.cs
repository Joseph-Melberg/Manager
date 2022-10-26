using Melberg.Application;
using Melberg.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.RemoteLogReaderService;

public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        RabbitModule.RegisterConsumer<Processor>(services);
        ApplicationModule.AddKeepAlive(services);
        Register.RegisterServices(services);
    }
}