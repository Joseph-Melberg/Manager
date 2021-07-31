using Inter.Domain;
using Inter.HeartbeatListenerAppService.Messages;

namespace Inter.HeartbeatListenerAppService.Mappers
{
    public static class HearbeatMessageMapper
    {
        public static Heartbeat ToDomain(this HeartbeatMessage message)
        {
            if(message == null)
            {
                return null;
            }
            return new Heartbeat
            {
                mac = message.Mac,
                name = message.Name
            };
        }
    }
}