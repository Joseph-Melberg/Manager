using System;
using System.Threading;
using System.Threading.Tasks;
using Inter.Common.Clock;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;

public class MetronomeService : IMetronomeService
{
    private readonly IClock _clock;
    private readonly IMetronomeInfrastructureService _infrastructureService;
    public MetronomeService(
        IMetronomeInfrastructureService infrastructureService,
        IClock clock)
    {
       _infrastructureService = infrastructureService; 
       _clock = clock;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while(!cancellationToken.IsCancellationRequested)
        {
            await SleepTillNextSecond();

            _infrastructureService.SendTick();

            var time = _clock.GetUtcNow().Second;

            if(time == 0)
            {
                _infrastructureService.SendMinuteTick();
            }
            //Future me, listen up
            //Make an IClock to hide the Datetime for testing purposes.

            //Also test it.

            Console.WriteLine("Tick sent");
        }
    }    

    private async Task SleepTillNextSecond()
    {
        await Task.Delay(1000 - DateTime.UtcNow.Millisecond);
    }
}
