using Melberg.Infrastructure.Rabbit.Messages;

namespace Inter.Infrastructure.Rabbit.Messages;

public class TickMessage : StandardMessage
{
    public TickMessage() 
    {
        Timestamp = DateTime.UtcNow;
    }

    public DateTime Timestamp {get; private set;}
    public override string GetRoutingKey() => "tick.second";
}
