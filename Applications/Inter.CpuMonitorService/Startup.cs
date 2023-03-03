using Melberg.Application;
using Melberg.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.CpuMonitorService;

public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        RabbitModule.RegisterConsumer<Processor>(services);
        Register.RegisterServices(services);
    }
}