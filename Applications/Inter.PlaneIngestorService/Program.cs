using Inter.DomainServices.Core;
using MelbergFramework.Application;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Inter.PlaneIngestorService;

class Program
{
    static async Task Main()
    {
        var servies =  MelbergHost.CreateDefaultApp<Startup>().Build().Services;
        var dom = servies.GetService<IPlaneIngestorDomainService>();
        var serix = servies.GetServices<IHostedService>();
        await MelbergHost
            .CreateDefaultApp<Startup>()
            .Build()
            .Begin(CancellationToken.None);
    }
}
