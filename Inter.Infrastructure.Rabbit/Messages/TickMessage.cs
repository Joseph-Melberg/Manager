using Melberg.Infrastructure.Rabbit.Messages;

namespace Inter.Infrastructure.Rabbit.Messages;

public class TickMessage : StandardMessage
{
    public TickMessage() { }

    public override string GetRoutingKey() => "tick.second";
}