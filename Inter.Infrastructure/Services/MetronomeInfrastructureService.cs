using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;

public class MetronomeInfrastructureService : IMetronomeInfrastructureService
{
    private readonly ITickPublisher _publisher;
    public MetronomeInfrastructureService(ITickPublisher publisher)
    {
        _publisher = publisher;
    }
    public void SendTick() => _publisher.SendTick(); 
}
