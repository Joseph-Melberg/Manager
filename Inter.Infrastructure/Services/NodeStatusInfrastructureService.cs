using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class NodeStatusInfrastructureService : INodeStatusInfrastructureService
    {
        private readonly IHeartbeatRepository _heartbeatRepository;
        public NodeStatusInfrastructureService(IHeartbeatRepository heartbeatRepository)
        {
            _heartbeatRepository = heartbeatRepository;
        }


        public async Task<List<Heartbeat>> GetStatiAsync()
        {
            return await _heartbeatRepository.GetStatiAsync();
        }

    }
}