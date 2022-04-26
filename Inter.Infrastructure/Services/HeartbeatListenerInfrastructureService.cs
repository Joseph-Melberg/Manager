using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;
public class HeartbeatListenerInfrastructureService : IHeartbeatListenerInfrastructureService
{
    private readonly INodeLifeRepository _nodeLifeRepository;
    private readonly IHeartbeatRepository _heartbeatRepository;
    public HeartbeatListenerInfrastructureService(
        IHeartbeatRepository heartBeatRepository,
        INodeLifeRepository nodeLifeRepository)
    {
        _nodeLifeRepository = nodeLifeRepository;
        _heartbeatRepository = heartBeatRepository;
    }

    public async Task<bool> GetHeartbeatStateAsync(string name)
    {
        var result = await _heartbeatRepository.GetStatusAsync(name);
        if(result == null)
        {
            return false;
        }
        return result.online;
    }
    public async Task UpdateAsync(Heartbeat heartBeat)
    {
        //First, mark how it was.
        await _nodeLifeRepository.MarkStatusAsync(heartBeat.name,!heartBeat.online);
        //Then, mark how it is. (this makes the graphs prettier)
        await _nodeLifeRepository.MarkStatusAsync(heartBeat.name,heartBeat.online);
        await _heartbeatRepository.UpdateAsync(heartBeat);
    }
}