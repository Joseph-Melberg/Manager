using System;
using System.Threading.Tasks;
using Inter.Common.Configuration;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Domain;
using System.Linq;

namespace Inter.DomainServices;
public class LifeAlertService : ILifeAlertService
{
    private readonly ILifeAlertInfrastructureService _infra;
    private readonly ILifeAlertRateConfiguration _rateConfig;
    public LifeAlertService(
        ILifeAlertInfrastructureService infrastructureService,
        ILifeAlertRateConfiguration rateConfiguration
    )
    {
        _infra = infrastructureService;
        _rateConfig = rateConfiguration;
    }

    public async Task Do()
    {
        Console.WriteLine("LifeAlert triggered");
        var stati = await _infra.GetStatusesAsync();
        foreach(var status in stati)
        {
            await ProcessStatus(status);
        }
    }

    private Task ProcessStatus(Heartbeat nodeState)
    {
        var announcedState = nodeState.announced;
        var isStale = IsStale(nodeState);
        var isAlive = nodeState.online;
        if (isAlive && isStale)
        {
            return UpdateAndAnnounceDeadNodeAsync(nodeState);
        }
        else if (!announcedState && !isStale)
        {
            return UpdateAndAnnounceLiveNodeAsync(nodeState);
        }
        return Task.CompletedTask;
    }
    private Task UpdateAndAnnounceDeadNodeAsync(Heartbeat nodeState)
    {
        nodeState.announced = false;
        nodeState.online = false;
        Console.WriteLine($"{nodeState.name} is offline");
        return Task.WhenAll(MarkStateChange(nodeState.name, nodeState.online),_infra.UpdateNodeAsync(nodeState));
    }
    private Task UpdateAndAnnounceLiveNodeAsync(Heartbeat nodeState)
    {
        nodeState.announced = true;
        nodeState.online = true;
        Console.WriteLine($"{nodeState.name} is online");
        return Task.WhenAll(MarkStateChange(nodeState.name, nodeState.online),_infra.UpdateNodeAsync(nodeState));
    }

    
    private async Task MarkStateChange(string name, bool finalStatus) 
    {
       await _infra.MarkStateAsync(new NodeStatus() {Name = name, Online = !finalStatus}); 
       await Task.Delay(10);
       await _infra.MarkStateAsync(new NodeStatus() {Name = name, Online = finalStatus}); 
    } 

    private bool IsStale(Heartbeat nodeState) => nodeState.timestamp.AddMinutes(_rateConfig.Rate) < DateTime.Now;
}