using Inter.Dependency;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.MetronomeService;

public class Register
{
    public static ServiceCollection RegisterServices(ServiceCollection services)
    {
        services.RegisterMetronomeService();

        return services;
    }
}