using MelbergFramework.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.TempLoggerAppService;

public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        Register.RegisterServices(services);
    }
}