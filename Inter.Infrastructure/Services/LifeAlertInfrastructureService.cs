using Inter.Infrastructure.Core;
using System.Collections.Generic;
using Inter.Infrastructure.Corral;
using Inter.Domain;
using System.Threading.Tasks;

namespace Inter.Infrastructure.Services;
public class LifeAlertInfrastructureService : ILifeAlertInfrastructureService
{
    private readonly IHeartbeatRepository _heartbeatRepository;
    private readonly INodeStateMarkRepository _nodeStateMarkRepository;
    public LifeAlertInfrastructureService(
        IHeartbeatRepository heartbeatRepository,
        INodeStateMarkRepository nodeStateMarkRepository
        )
    {
        _heartbeatRepository = heartbeatRepository;
        _nodeStateMarkRepository = nodeStateMarkRepository;
    }

    public Task<List<Heartbeat>> GetStatusesAsync() => _heartbeatRepository.GetStatusesAsync();

    public Task UpdateNodeAsync(Heartbeat model) => _heartbeatRepository.UpdateAsync(model); 
    public Task MarkStateAsync(NodeStatus status) => _nodeStateMarkRepository.MarkNodeStatusAsync(status);
}