using Inter.Domain;
using Inter.Infrastructure.Redis.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

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

    public static PlaneModel ToModel(this RedisValue value)
    {
        if(string.IsNullOrEmpty((string)value))
        {
            return null;
        }
        var dto = JsonConvert.DeserializeObject<PlaneModel>((string)value);
        var plane = new PlaneModel
        {
            altitude = dto.altitude,
            category = dto.category,
            flight = dto.flight,
            hexValue = dto.hexValue,
            lat = dto.lat,
            lon = dto.lon,
            nucp = dto.nucp,
            rssi = dto.rssi,
            speed = dto.speed,
            squawk = dto.squawk,
            track = dto.track,
            vert_rate = dto.vert_rate
        };
        return plane;
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
    public static string ToPayload(this PlaneModel plane) => JsonConvert.SerializeObject(plane);
}