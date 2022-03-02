using Inter.Infrastructure.Corral;
using Inter.Infrastructure.Rabbit.Messages;
using Melberg.Core.Rabbit.Configurations;
using Melberg.Infrastructure.Rabbit.Publishers;

namespace Inter.Infrastructure.Rabbit.Publishers;

public class TickPublisher : ITickPublisher
{
    private readonly IStandardPublisher<TickMessage> _publisher;
    public TickPublisher(IStandardPublisher<TickMessage> publisher) 
    {
        _publisher = publisher;
    }


    public void SendTick()
    {
        var mark = new TickMessage();
        _publisher.Send(mark);
    }
}