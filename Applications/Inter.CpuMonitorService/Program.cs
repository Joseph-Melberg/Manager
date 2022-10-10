using Melberg.Application;

namespace Inter.CpuMonitorService;

class Program
{
    static async Task Main(string[] args) => await MelbergHost.CreateDefaultApp<Startup>().Build().StartAsync();
}