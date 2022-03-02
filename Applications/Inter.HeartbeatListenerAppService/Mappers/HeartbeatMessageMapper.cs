using Inter.Domain;
using Inter.HeartbeatListenerAppService.Messages;

namespace Inter.HeartbeatListenerAppService.Mappers;

public static class HeartbeatMessageMapper
{
    public static HeartbeatPayload ToDomain(this HeartbeatMessage message)
    {
        if(message == null)
        {
            return null;
        }

        return new HeartbeatPayload
        {
            Mac = message.Mac,
            Name = message.Name
        };
    }
}