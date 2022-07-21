namespace Inter.Infrastructure.Corral;

public interface IMinutePublisher
{
    public void SendTick();
}