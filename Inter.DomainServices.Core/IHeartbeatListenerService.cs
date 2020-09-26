using Inter.Domain;

namespace Inter.DomainServices.Core
{
    public interface IHeartbeatListenerService
    {
        void Process(HeartbeatMessage message);
    }
}