using Inter.Domain;
using Inter.Infrastructure.Redis.Models;

namespace Inter.Infrastructure.Redis.Mappers;

public static class PlaneMapper
{
    public static PlaneModel ToModel(this Plane model)
    {
        if(model == null)
        {
            return null;
        }

        return new PlaneModel()
        {
            altitude = model.altitude,
            category = model.category,
            flight = model.flight,
            hexValue = model.hexValue,
            lat = model.lat,
            lon = model.lon,
            messages = model.messages,
            nucp = model.nucp,
            rssi = model.rssi,
            speed = model.speed,
            squawk = model.squawk,
            track = model.track,
            vert_rate = model.vert_rate
        };
    }
    public static Plane ToDomain(this PlaneModel model)
    {
        if(model == null)
        {
            return null;
        }

        return new Plane()
        {
            altitude = model.altitude,
            category = model.category,
            flight = model.flight,
            hexValue = model.hexValue,
            lat = model.lat,
            lon = model.lon,
            messages = model.messages,
            nucp = model.nucp,
            rssi = model.rssi,
            speed = model.speed,
            squawk = model.squawk,
            track = model.track,
            vert_rate = model.vert_rate
        };
    }
}