using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Redis.Contexts;
using Inter.Infrastructure.Redis.Mappers;
using Melberg.Infrastructure.Redis.Repository;
using Newtonsoft.Json;

namespace Inter.Infrastructure.Redis.Repositories;
public class PlaneCacheRepository : RedisRepository<PlaneCacheContext>, IPlaneCacheRepository
{
    private TimeSpan FrameLifespan => new System.TimeSpan(0,0,45);
    public PlaneCacheRepository(PlaneCacheContext context) : base(context) { }

    public async Task<PlaneFrame> GetPlaneFrameAsync(long timestamp)
    {
        var key = $"plane_congregated_{timestamp}";
        var payload = await DB.StringGetAsync(key);
        var planes = payload.ToDomain("aggregate","aggregate");
        return planes; 
    }


    public async Task InsertCongregatedPlaneFrameAsync(PlaneFrame frame) => await DB.StringSetAsync(ToCongregatedKey(frame),frame.ToModel().ToPayload(),new TimeSpan(0,0,45));
    public async Task InsertPlaneFrameAsync(PlaneFrame frame) => await DB.StringSetAsync(ToKey(frame),frame.ToModel().ToPayload(),new TimeSpan(0,0,45));

    public async Task InsertPreCongregatedPlaneFrameAsync(PlaneFrame planeFrame)
    {
        await DB.StringSetAsync(ToPreAggregateKey(planeFrame.Source,planeFrame.Antenna,planeFrame.Now),planeFrame.ToModel().ToPayload(),TimeSpan.FromSeconds(50));
    }


    public async IAsyncEnumerable<PlaneFrame> GetPreCongregatedPlaneFramesAsync(long timestamp)
    {
        await foreach(var key in Server.KeysAsync(pattern:$"plane_preaggregate_*_*_{timestamp}"))
        {
            var keySections = key.ToString().Split("_");
            yield return (await DB.StringGetAsync(key)).ToDomain(keySections[2],keySections[3]);
        }
        yield break;
    }
    public string ToKey( PlaneFrame frame) => $"plane_{frame.Now}";
    public string ToCongregatedKey( PlaneFrame frame) => $"plane_congregated_{frame.Now}";

    public string ToPreAggregateKey(string source, string antenna, long timestamp) => $"plane_preaggregate_{source}_{antenna}_{timestamp}";


    public async Task<PlaneFrame> GetPreCongregatedPlaneFrameAsync(string source, string antenna, long timestamp) =>
        (await DB.StringGetAsync(ToPreAggregateKey(source,antenna,timestamp))).ToDomain(source,antenna);
}