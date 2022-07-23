using System;
using System.Threading.Tasks;
using Inter.Common.Configuration;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Domain;
using System.Linq;
using System.Collections.Generic;

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
        await Task.WhenAll(await UpdateLifeStates(await _infra.GetStatusesAsync()).ToArrayAsync());
    }
    private async IAsyncEnumerable<Task> UpdateLifeStates(IEnumerable<Heartbeat> states)
    {
        foreach(var state in states)
        {
            //This can't be parallelized because the MySQL connection doesn't allow it.
            yield return await ProcessStatus(state);
        }
    }
    private Task<Task> ProcessStatus(Heartbeat nodeState)
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
    private async Task<Task> UpdateAndAnnounceDeadNodeAsync(Heartbeat nodeState)
    {
        nodeState.announced = false;
        nodeState.online = false;
        Console.WriteLine($"{nodeState.name} is offline");
        await _infra.UpdateNodeAsync(nodeState);
        return MarkStateChange(nodeState.name, nodeState.online);
    }
    private async Task<Task> UpdateAndAnnounceLiveNodeAsync(Heartbeat nodeState)
    {
        nodeState.announced = true;
        nodeState.online = true;
        Console.WriteLine($"{nodeState.name} is online");
        await _infra.UpdateNodeAsync(nodeState);
        return MarkStateChange(nodeState.name, nodeState.online);
    }

    
    private async Task MarkStateChange(string name, bool finalStatus) 
    {
       await _infra.MarkStateAsync(new NodeStatus() {Name = name, Online = !finalStatus}); 
       await Task.Delay(100);
       await _infra.MarkStateAsync(new NodeStatus() {Name = name, Online = finalStatus}); 
    } 

    private bool IsStale(Heartbeat nodeState) => nodeState.timestamp.AddMinutes(_rateConfig.Rate) < DateTime.Now;
}