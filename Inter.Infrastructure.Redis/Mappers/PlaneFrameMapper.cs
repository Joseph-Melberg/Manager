using System.Linq;
using Inter.Domain;
using Inter.Infrastructure.Redis.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Inter.Infrastructure.Redis.Mappers;
public static class PlaneFrameMapper
{
    public static PlaneFrame ToDomain(this RedisValue value,string source, string antenna)
    {
        if(string.IsNullOrEmpty((string)value))
        {
            return new PlaneFrame
            {
                Antenna = antenna,
                Source = source,
                Planes = new Plane[0],
                Now = 0
            };
        }
        var dto = JsonConvert.DeserializeObject<PlaneFrameModel>((string)value);
        var frame = new PlaneFrame
        {
            Antenna = antenna,
            Source = source,
            Planes = dto.Planes.Select(_ => PlaneMapper.ToDomain(_)).ToArray(),
            Now = dto.Now
        };
        return frame;
    }

    public static PlaneFrameModel ToModel(this PlaneFrame frame)
    {
        if(frame == null)
        {
            return null;
        }
        return new PlaneFrameModel
        {
            Now = frame.Now,
            Planes = frame.Planes.Select(_ => _.ToModel()).ToArray()
        };
    }
    public static string ToPayload(this PlaneFrameModel frame) => JsonConvert.SerializeObject(frame);
}