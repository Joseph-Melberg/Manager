using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;
public class NodeApiInfrastructureService : INodeApiInfrastructureService
{
    private readonly IHeartbeatRepository _heartbeatRepository;
    public NodeApiInfrastructureService(IHeartbeatRepository heartbeatRepository)
    {
        _heartbeatRepository = heartbeatRepository;
    }

    public Task<List<Heartbeat>> GetStatiAsync() => _heartbeatRepository.GetStatusesAsync();
}