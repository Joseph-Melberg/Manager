using MelbergFramework.Application;
using MelbergFramework.Infrastructure.Rabbit.Metrics;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.PlaneCongregatorService;

class Program
{
    static async Task Main(string[] args)  
    {
        await MelbergHost
            .CreateDefaultApp<Startup>()
            .Build()
            .Begin(CancellationToken.None);

        //await j;
}
    }