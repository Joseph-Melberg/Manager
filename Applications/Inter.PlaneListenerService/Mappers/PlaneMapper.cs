using Inter.Domain;
using Inter.PlaneListenerService.Models;

namespace Inter.PlaneListenerService.Mappers;
public static class PlaneMapper
{
    public static Plane ToDomain(this AirplaneData data)
    {
       if(data == null) return null;
       return new Plane
       {
            Altitude = data.altitude,
            Category = data.category,
            Flight = data.flight,
            HexValue = data.hex,
            Latitude = data.lat,
            Longitude = data.lon,
            Messages = data.messages,
            Nucp = data.nucp,
            Rssi = data.rssi,
            Speed = data.speed,
            Squawk = data.squawk,
            Track = data.track,
            VerticleRate = data.vert_rate
       };
    }
}