using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class HeartbeatListenerInfrastructureService : IHeartbeatListenerInfrastructureService
    {
        private readonly IHeartbeatRepository _heartbeatRepository;
        public HeartbeatListenerInfrastructureService(IHeartbeatRepository heartBeatRepository)
        {
            _heartbeatRepository = heartBeatRepository;
        }

        public bool GetHeartbeatState(string name)
        {
            return _heartbeatRepository.GetState(name);
        }
        public async Task UpdateAsync(HeartbeatModel heartBeat)
        {
            await _heartbeatRepository.UpdateAsync(heartBeat);
        }
    }
}