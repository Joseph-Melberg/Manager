using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core;
public interface IHeartbeatListenerDomainService
{
    Task Process(HeartbeatPayload message);
}