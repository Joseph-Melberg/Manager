using MelbergFramework.Application;

namespace Inter.CpuMonitorService;

class Program
{
    static async Task Main(string[] args) => await MelbergHost.CreateDefaultApp<Startup>().Build().Begin(CancellationToken.None);
}