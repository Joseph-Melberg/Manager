using Melberg.Application;
using Melberg.Infrastructure.Rabbit.Consumers;

namespace Inter.PlaneCongregatorService;

class Program
{
    static async Task Main(string[] args) => await MelbergHost.CreateDefaultApp<Startup>().Build().Begin(CancellationToken.None);
}