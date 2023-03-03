using Melberg.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.MetronomeService;


public class Startup : IAppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        Register.RegisterServices(services);
    }
}