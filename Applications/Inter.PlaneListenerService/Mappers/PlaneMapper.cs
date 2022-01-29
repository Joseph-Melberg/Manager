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
            altitude = data.altitude,
            category = data.category,
            flight = data.flight,
            hexValue = data.hex,
            lat = data.lat,
            lon = data.lon,
            messages = data.messages,
            nucp = data.nucp,
            rssi = data.rssi,
            speed = data.speed,
            squawk = data.squawk,
            track = data.track,
            vert_rate = data.vert_rate
       };
    }
}