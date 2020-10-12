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
            var result = _heartbeatRepository.GetStatus(name);
            if(result == null)
            {
                return false;
            }
            return result.online;
        }
        public Task UpdateAsync(HeartbeatModel heartBeat)
        {
            return _heartbeatRepository.UpdateAsync(heartBeat);
        }
    }
}