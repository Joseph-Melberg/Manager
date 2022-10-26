using Melberg.Infrastructure.Rabbit.Messages;

namespace Inter.RemoteLogReaderService.Messsage;

public class LogMessage : StandardMessage
{
    public override string GetRoutingKey()
    {
        throw new NotImplementedException();
    }
}