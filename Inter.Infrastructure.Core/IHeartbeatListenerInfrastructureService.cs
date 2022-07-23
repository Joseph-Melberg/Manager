using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;
public interface IHeartbeatListenerInfrastructureService
{
    Task<Heartbeat> GetHeartbeatStateAsync(string name);
    Task UpdateAsync(Heartbeat heartBeat);

}