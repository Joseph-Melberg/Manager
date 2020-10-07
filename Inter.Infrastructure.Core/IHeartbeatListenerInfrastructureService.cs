using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core
{
    public interface IHeartbeatListenerInfrastructureService
    {
        bool GetHeartbeatState(string name);
        Task UpdateAsync(HeartbeatModel heartBeat);
    }
}
