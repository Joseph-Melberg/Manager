using System.Linq;
using Inter.Domain;
using Inter.PlaneListenerService.Messages;
using Inter.PlaneListenerService.Models;

namespace Inter.PlaneListenerService.Mappers;

public static class PlaneMessageMapper
{
    public static PlaneFrame ToDomain(this PlaneMessage message)
    {
        if(message == null)
        {
            return null;
        }

        return new PlaneFrame
        {
            Antenna = message.Antenna,
            Now = (long)message.Now,
            Planes = message.Planes.Where(_ => _.IsValid()).Select(_ => _.ToDomain()).ToArray(),
            Source = message.Source
        };
    }
    private static bool IsValid(this AirplaneData data) =>
        data.lat.HasValue &&
        data.lon.HasValue &&
        data.altitude.HasValue &&
        data.vert_rate.HasValue &&
        data.track.HasValue &&
        data.speed.HasValue &&
        data.rssi.HasValue
    ;
}