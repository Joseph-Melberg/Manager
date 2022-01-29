using Inter.Domain;
using Inter.PlaneIngestorService.Models;

namespace Inter.PlaneIngestorService.Mappers;
public static class PlaneMapper
{
    public static Plane ToDomain(this AirplaneData data)
    {
       if(data == null) return null;
       return new Plane
       {
            altitude = data.altitude ?? null,
            category = data.category,
            flight = data.flight,
            hexValue = data.hex,
            lat = data.lat ?? null,
            lon = data.lon ?? null,
            messages = data.messages,
            nucp = data.nucp,
            rssi = data.rssi ?? null,
            speed = data.speed ?? null,
            squawk = data.squawk,
            track = data.track ?? null,
            vert_rate = data.vert_rate ?? null
       };
    }
}