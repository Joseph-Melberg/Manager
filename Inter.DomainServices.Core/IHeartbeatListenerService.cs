using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core;
public interface IHeartbeatListenerService
{
    Task Process(HeartbeatPayload message);
}