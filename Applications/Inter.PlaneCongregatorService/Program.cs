using MelbergFramework.Application;
using MelbergFramework.Infrastructure.Rabbit.Metrics;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.PlaneCongregatorService;

class Program
{
    static async Task Main(string[] args)  
    {
        var j = MelbergHost
            .CreateDefaultApp<Startup>()
            .Build();
           var p =  j.Services.GetService<IMetricPublisher>();
           p.SendMetric(4000,DateTime.UtcNow);
           await Task.Delay(10);
           p.SendMetric(4000,DateTime.UtcNow);
           await Task.Delay(10);
           p.SendMetric(4000,DateTime.UtcNow);
           await Task.Delay(10);
           p.SendMetric(4000,DateTime.UtcNow);
           await Task.Delay(10);
           p.SendMetric(4000,DateTime.UtcNow);
           await Task.Delay(10);
           p.SendMetric(4000,DateTime.UtcNow);
           await Task.Delay(10);
           p.SendMetric(4000,DateTime.UtcNow);
           await Task.Delay(10);
           p.SendMetric(4000,DateTime.UtcNow);
           await Task.Delay(10);
           p.SendMetric(4000,DateTime.UtcNow);
           await Task.Delay(10);
           p.SendMetric(4000,DateTime.UtcNow);
           p.SendMetric(4000,DateTime.UtcNow);
           p.SendMetric(4000,DateTime.UtcNow);
           p.SendMetric(4000,DateTime.UtcNow);
           p.SendMetric(4000,DateTime.UtcNow);
           p.SendMetric(4000,DateTime.UtcNow);
           p.SendMetric(4000,DateTime.UtcNow);
            //.Begin(CancellationToken.None);

        //await j;
}
    }