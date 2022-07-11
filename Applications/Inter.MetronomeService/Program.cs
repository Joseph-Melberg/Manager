using System;
using System.IO;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.MetronomeService;
class Program
{
    public static IConfigurationRoot configuration;

    private static IServiceProvider _serviceProvider;
    static async Task Main(string[] args)
    {
        RegisterServices();

        var service = _serviceProvider.GetRequiredService<IMetronomeService>();

        await service.StartAsync(new System.Threading.CancellationToken());

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