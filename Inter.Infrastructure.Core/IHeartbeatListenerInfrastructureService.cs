using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core
{
    public interface IHeartbeatListenerInfrastructureService
    {
        Task UpdateAsync(HeartbeatModel heartBeat);
    }
}
