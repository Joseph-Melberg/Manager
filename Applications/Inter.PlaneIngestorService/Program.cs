using Microsoft.Extensions.Configuration;
using Melberg.Infrastructure.Rabbit.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.PlaneIngestorService;

class Program
{

    public static IConfigurationRoot configuration;
    private static IServiceProvider _serviceProvider;
    static async Task Main(string[] args)
    {
        RegisterServices();
        await _serviceProvider.GetRequiredService<IStandardRabbitService>().Run();
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

