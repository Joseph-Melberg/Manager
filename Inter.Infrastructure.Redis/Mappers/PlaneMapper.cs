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
            altitude = model.Altitude,
            category = model.Category,
            flight = model.Flight,
            hexValue = model.HexValue,
            lat = model.Latitude,
            lon = model.Longitude,
            messages = model.Messages,
            nucp = model.Nucp,
            rssi = model.Rssi,
            speed = model.Speed,
            squawk = model.Squawk,
            track = model.Track,
            vert_rate = model.VerticleRate
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
    public static TimeAnotatedPlane ToDomain(this PlaneModel model)
    {
        if(model == null)
        {
            return null;
        }

        return new TimeAnotatedPlane()
        {
            Altitude = model.altitude,
            Category = model.category,
            Flight = model.flight,
            HexValue = model.hexValue,
            Latitude = model.lat,
            Longitude = model.lon,
            Messages = model.messages,
            Nucp = model.nucp,
            Rssi = model.rssi,
            Speed = model.speed,
            Squawk = model.squawk,
            Track = model.track,
            VerticleRate = model.vert_rate
        };
    }
    public static string ToPayload(this PlaneModel plane) => JsonConvert.SerializeObject(plane);
}