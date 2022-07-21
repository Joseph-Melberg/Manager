namespace Inter.Infrastructure.Core;

public interface IMetronomeInfrastructureService
{
    void SendTick();
    void SendMinuteTick();
}