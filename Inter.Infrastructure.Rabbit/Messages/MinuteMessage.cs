namespace Inter.Infrastructure.Rabbit.Messages;

public class MinuteMessage : TickMessage
{
    public override string GetRoutingKey() => "tick.minute";
}