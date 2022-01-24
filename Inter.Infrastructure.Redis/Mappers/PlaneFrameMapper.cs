using Inter.Domain;
using Inter.Infrastructure.Redis.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Inter.Infrastructure.Redis.Mappers;
public static class PlaneFrameMapper
{
    public static PlaneFrame ToDomain(this RedisValue value, int now, string an)
    {
        var planes = JsonConvert.DeserializeObject<Plane[]>((string)value);
        var frame = new PlaneFrame
        {
            Planes = planes,
            Now = now
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
            Planes = frame.Planes
        };
    }
    
    public static string ToKey(this PlaneFrame frame) => $"plane_{frame.Now}";
    public static string ToPayload(this PlaneFrameModel frame) => JsonConvert.SerializeObject(frame);
}