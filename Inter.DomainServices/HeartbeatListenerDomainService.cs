using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;
public class HeartbeatListenerDomainService : IHeartbeatListenerDomainService
{
    private readonly IHeartbeatListenerInfrastructureService _infraservice;
    public HeartbeatListenerDomainService(IHeartbeatListenerInfrastructureService infrastructureService)
    {
        _infraservice = infrastructureService;
    }

    public async Task Process(HeartbeatPayload message)
    {
        Heartbeat currentState = await _infraservice.GetHeartbeatStateAsync(message.Name);
        
        var model = new Heartbeat()
        {
            name = Truncate(message.Name),
            mac = message.Mac,
            timestamp = DateTime.Now,
            announced = currentState?.announced ?? false,
            online = true
        };
        await _infraservice.UpdateAsync(model);
        Console.WriteLine($"Heartbeat from {message.Name} was proccessed");
    }

    private string Truncate(string value) => value.Substring(0, Math.Min(value.Length, 15));
}