using Inter.Domain;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Inter.Infrastructure.Redis.Mappers
{
    public static class PlaneFrameMapper
    {
        public static PlaneFrame ToDomain(this RedisValue value, int now)
        {
            var planes = JsonConvert.DeserializeObject<Plane[]>((string)value);
            var frame = new PlaneFrame
            {
                Planes = planes,
                Now = now
            };
            return frame;
        }
        
        public static string ToKey(this PlaneFrame frame) => $"plane_{frame.Now}";
        public static string ToPayload(this PlaneFrame frame) => JsonConvert.SerializeObject(frame.Planes);
    }
}