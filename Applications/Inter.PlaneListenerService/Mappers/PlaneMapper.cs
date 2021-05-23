
using Inter.Domain;
using Inter.PlaneListenerService.Models;

namespace Inter.PlaneListenerService.Mappers
{
    public static class PlaneMapper
    {
        public static Plane ToDomain(this AirplaneData data)
        {
           if(data == null) return null;
           return new Plane
           {
                altitude = data.altitude.Value,
                category = data.category,
                flight = data.flight,
                hexValue = data.hex,
                lat = data.lat.Value,
                lon = data.lon.Value,
                messages = data.messages,
                nucp = data.nucp,
                rssi = data.rssi.Value,
                speed = data.speed.Value,
                squawk = data.squawk,
                track = data.track.Value,
                vert_rate = data.vert_rate.Value
           };
        }
    }
}