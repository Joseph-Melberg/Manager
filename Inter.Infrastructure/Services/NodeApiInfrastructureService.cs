using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class NodeApiInfrastructureService : INodeApiInfrastructureService
    {

        private readonly IHeartbeatRepository _heartBeatRepository;
        public NodeApiInfrastructureService(IHeartbeatRepository heartbeatRepository)
        {
            _heartBeatRepository = heartbeatRepository;
        }
        public Task<List<Heartbeat>> GetStatiAsync() => _heartBeatRepository.GetStatiAsync();
        
    }
}