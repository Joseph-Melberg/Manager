using System.Threading;
using System.Threading.Tasks;
using MelbergFramework.Application;

namespace Inter.MetricMonitorAppService;

class Program
{
    static async Task Main(string[] args) => 
        await MelbergHost.CreateDefaultApp<Startup>()
        .Build()
        .Begin(CancellationToken.None);
}