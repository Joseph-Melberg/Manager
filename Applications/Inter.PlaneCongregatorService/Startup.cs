using Melberg.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.PlaneCongregatorService;

public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        ApplicationModule.AddKeepAlive(services);
        Register.RegisterServices(services);
    }
}