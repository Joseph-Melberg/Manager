using System.Linq;
using Inter.Domain;
using Inter.Infrastructure.Redis.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Inter.Infrastructure.Redis.Mappers;
public static class PlaneFrameMapper
{
    public static PlaneFrame ToDomain(this RedisValue value,PlaneSourceDefintion source)
    {
        if(string.IsNullOrEmpty((string)value))
        {
            return new PlaneFrame
            {
                Antenna = source.Antenna,
                Source = source.Node,
                Planes = new TimeAnotatedPlane[0],
                Now = 0
            };
        }
        var dto = JsonConvert.DeserializeObject<PlaneFrameModel>((string)value);
        var frame = new PlaneFrame
        {
            Antenna = source.Antenna,
            Source = source.Node,
            Planes = dto.Planes.Select(_ => PlaneMapper.ToDomain(_)).ToArray(),
            Now = dto.Now
        };
        return frame;
    }
    public static PlaneFrameDelta ToDomainWithDelta(this RedisValue value,PlaneSourceDefintion source)
    {
        if(string.IsNullOrEmpty((string)value))
        {
            return null;
        }
        var dto = JsonConvert.DeserializeObject<PlaneFrameDeltaModel>((string)value);
        var frame = new PlaneFrameDelta
        {
            Antenna = source.Antenna,
            Source = source.Node,
            Interval = dto.Interval,
            Planes = dto.Planes.Select(_ => PlaneMapper.ToDomain(_)).ToArray(),
            Now = dto.Now
        };
        return frame;
    }

    public static PlaneFrameDeltaModel ToModel(this PlaneFrameDelta frame)
    {
        if(frame == null)
        {
            return null;
        }
        return new PlaneFrameDeltaModel
        {
            Now = frame.Now,
            Interval = frame.Interval,
            Planes = frame.Planes.Select(_ => _.ToModel()).ToArray()
        };
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
    public static string ToPayload(this PlaneFrameDeltaModel frame) => JsonConvert.SerializeObject(frame);
    public static string ToPayload(this PlaneFrameModel frame) => JsonConvert.SerializeObject(frame);
}