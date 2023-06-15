using MelbergFramework.Application;

namespace Inter.PlaneCongregatorService;

class Program
{
    static async Task Main(string[] args)  
    {
        var j = MelbergHost
            .CreateDefaultApp<Startup>()
            .Build()
            .Begin(CancellationToken.None);

        await j;
}
    }