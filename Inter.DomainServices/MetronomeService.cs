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
    public async Task Start()
    {
        while(true)
        {
            await SleepTillNextSecond();
            _infrastructureService.SendTick();
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