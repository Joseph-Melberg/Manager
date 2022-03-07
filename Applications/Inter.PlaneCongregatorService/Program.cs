using Inter.DomainServices.Core;
using Melberg.Infrastructure.Rabbit.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.PlaneCongregatorService;

class Program
{

    public static IConfigurationRoot configuration;
    private static IServiceProvider _serviceProvider;
    static async Task Main(string[] args)
    {
        RegisterServices();
        try
        {
            //await _serviceProvider.GetService<IPlaneCongregatorService>().CongregatePlaneInfo(DateTimeOffset.Now.ToUnixTimeSeconds());
            await _serviceProvider.GetRequiredService<IStandardRabbitService>().Run();
            
        }
        catch (System.Exception ex)
        {
            
        }
        DisposeServices();
    }

    private static void RegisterServices()
    {

        var services = new ServiceCollection();
        configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            .AddJsonFile("appsettings.json", false)
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        Register.RegisterServices(services);
        _serviceProvider = services.BuildServiceProvider();
    }

    private static void DisposeServices()
    {
        if (_serviceProvider == null)
        {
            return;
        }
        if (_serviceProvider is IDisposable)
        {
            ((IDisposable)_serviceProvider).Dispose();
        }
    }
}