using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;

public class MetronomeInfrastructureService : IMetronomeInfrastructureService
{
    private readonly ITickPublisher _secondPublisher;
    private readonly IMinutePublisher _minutePublisher;
    public MetronomeInfrastructureService(
        ITickPublisher secondPublisher,
        IMinutePublisher minutePublisher)
    {
        _secondPublisher = secondPublisher;
        _minutePublisher = minutePublisher;
    }
    public void SendTick() => _secondPublisher.SendTick(); 
    public void SendMinuteTick() => _minutePublisher.SendTick();
}
