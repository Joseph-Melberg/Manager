using System;
using System.Threading.Tasks;
using Inter.Common.Configuration;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Domain;
using System.Linq;
using System.Collections.Generic;

namespace Inter.DomainServices;
public class LifeAlertDomainService : ILifeAlertDomainService
{
    private readonly ILifeAlertInfrastructureService _infra;
    private readonly ILifeAlertRateConfiguration _rateConfig;
    public LifeAlertDomainService(
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
        foreach(var state in await _infra.GetStatusesAsync())
        {
            await ProcessStatus(state);
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
        return Task.FromResult(Task.CompletedTask);
    }
    private async Task UpdateAndAnnounceDeadNodeAsync(Heartbeat nodeState)
    {
        nodeState.announced = false;
        nodeState.online = false;
        Console.WriteLine($"{nodeState.name} is offline");
        await _infra.UpdateNodeAsync(nodeState);
        await MarkStateChange(nodeState.name, nodeState.online);
    }
    private async Task UpdateAndAnnounceLiveNodeAsync(Heartbeat nodeState)
    {
        nodeState.announced = true;
        nodeState.online = true;
        Console.WriteLine($"{nodeState.name} is online");
        await _infra.UpdateNodeAsync(nodeState);
        await MarkStateChange(nodeState.name, nodeState.online);
    }

    
    private async Task MarkStateChange(string name, bool finalStatus) 
    {
       await _infra.MarkStateAsync(new NodeStatus() {Name = name, Online = !finalStatus}); 
       await Task.Delay(1000);
       await _infra.MarkStateAsync(new NodeStatus() {Name = name, Online = finalStatus}); 
    } 

    private bool IsStale(Heartbeat nodeState) => nodeState.timestamp.AddMinutes(_rateConfig.Rate) < DateTime.Now;
}