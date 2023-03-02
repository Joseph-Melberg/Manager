using Melberg.Application;
using Melberg.Infrastructure.Rabbit;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.CpuMonitorService;

public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        RabbitModule.RegisterConsumer<Processor>(services);
        Console.Write("AAAA");
        T();
        //ApplicationModule.AddKeepAlive(services);
        Register.RegisterServices(services);
    }

    private void T()
    {
        var j = 1;
    }
}