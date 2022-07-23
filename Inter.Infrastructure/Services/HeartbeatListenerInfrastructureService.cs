using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;
public class HeartbeatListenerInfrastructureService : IHeartbeatListenerInfrastructureService
{
    private readonly IHeartbeatRepository _heartbeatRepository;
    public HeartbeatListenerInfrastructureService(
        IHeartbeatRepository heartBeatRepository
        )
    {
        _heartbeatRepository = heartBeatRepository;
    }

    public Task<Heartbeat> GetHeartbeatStateAsync(string name) =>
        _heartbeatRepository.GetStatusAsync(name);

    public Task UpdateAsync(Heartbeat heartBeat) => _heartbeatRepository.UpdateAsync(heartBeat);
}