using System;
using System.Threading;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;

public class MetronomeService : IMetronomeService
{
    private readonly IMetronomeInfrastructureService _infrastructureService;
    public MetronomeService(IMetronomeInfrastructureService infrastructureService)
    {
       _infrastructureService = infrastructureService; 
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while(!cancellationToken.IsCancellationRequested)
        {
            await SleepTillNextSecond();
            _infrastructureService.SendTick();
            Console.WriteLine("Tick sent");
        }
    }    

    private async Task SleepTillNextSecond()
    {
        await Task.Run(() =>
        {
            Thread.Sleep(1000 - DateTime.UtcNow.Millisecond);
        });
    }
}
