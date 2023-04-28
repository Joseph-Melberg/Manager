using MelbergFramework.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.HeartbeatListenerAppService;

public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        Register.RegisterServices(services);
    }
}