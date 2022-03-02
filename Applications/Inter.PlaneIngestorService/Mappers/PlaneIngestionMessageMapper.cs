using Inter.Domain;
using Inter.PlaneIngestorService.Messages;

namespace Inter.PlaneIngestorService.Mappers;

public static class PlaneIngestionMessageMapper
{
    public static PlaneFrame ToDomain(this PlaneIngestionMessage message)
    {
        if(message == null)
        {
            return null;
        }

        return new PlaneFrame
        {
            Antenna = message.Antenna,
            Now = (long)message.Now,
            Planes = message.Planes.Select(_ => _.ToDomain()).ToArray(),
            Source = message.Source
        };
    }
}