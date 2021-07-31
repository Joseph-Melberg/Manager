using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core
{
    public interface IHeartbeatListenerInfrastructureService
    {
        Task<bool> GetHeartbeatStateAsync(string name);
        Task UpdateAsync(HeartbeatModel heartBeat);
    }
}
