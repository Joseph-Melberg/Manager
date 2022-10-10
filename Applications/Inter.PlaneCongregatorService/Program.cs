using Melberg.Application;
namespace Inter.PlaneCongregatorService;

class Program
{
    static async Task Main(string[] args) => await MelbergHost.CreateDefaultApp<Startup>().Build().StartAsync();
}