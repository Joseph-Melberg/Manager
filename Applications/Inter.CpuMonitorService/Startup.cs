using Inter.CpuMonitorService.Messages;
using Melberg.Application;
using Melberg.Infrastructure.Rabbit;
using Melberg.Infrastructure.Rabbit.Translator;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.CpuMonitorService;

public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        RabbitModule.RegisterConsumer<Processor>(services);
        ApplicationModule.AddKeepAlive(services);
        Register.RegisterServices(services);
    }
}