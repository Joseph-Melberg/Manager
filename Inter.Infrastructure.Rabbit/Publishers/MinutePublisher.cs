using Inter.Infrastructure.Corral;
using Inter.Infrastructure.Rabbit.Messages;
using Melberg.Infrastructure.Rabbit.Publishers;

namespace Inter.Infrastructure.Rabbit.Publishers;

public class MinutePublisher : IMinutePublisher
{
    private readonly IStandardPublisher<MinuteMessage> _publisher;
    public MinutePublisher(IStandardPublisher<MinuteMessage> publisher)
    {
        _publisher = publisher;
    }

    public void SendTick() => _publisher.Send(new MinuteMessage());
}