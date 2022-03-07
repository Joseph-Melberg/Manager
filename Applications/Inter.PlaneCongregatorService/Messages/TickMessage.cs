using Melberg.Infrastructure.Rabbit.Messages;

namespace Inter.PlaneCongregatorService.Messages;

public class TickMessage : StandardMessage
{
   public DateTime Timestamp {get; private set;}

    public override string GetRoutingKey()
    {
        return null;
    }
}