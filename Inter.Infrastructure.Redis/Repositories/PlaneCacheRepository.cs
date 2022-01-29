using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.Redis.Contexts;
using Inter.Infrastructure.Redis.Mappers;
using Melberg.Infrastructure.Redis.Repository;

namespace Inter.Infrastructure.Redis.Repositories;
public class PlaneCacheRepository : RedisRepository<PlaneCacheContext>, IPlaneCacheRepository
{
    private TimeSpan FrameLifespan => new System.TimeSpan(0,0,45);
    public PlaneCacheRepository(PlaneCacheContext context) : base(context)
    {
    }

    public async Task<PlaneFrame> GetPlaneFrameAsync(long timestamp)
    {
        var key = $"plane_{timestamp}";
        var payload = await DB.StringGetAsync(key);
        var planes = payload.ToDomain("aggregate","aggregate");
        return planes; 
    }

    public Task InsertPlaneFrameAsync(PlaneFrame frame) => DB.StringSetAsync(ToKey(frame),frame.ToModel().ToPayload(),new TimeSpan(0,0,45));

    public async Task InsertPreHydratedPlaneFrameAsync(PlaneFrame planeFrame)
    {
        await DB.StringSetAsync(ToPreAggregateKey(planeFrame),planeFrame.ToModel().ToPayload(),TimeSpan.FromSeconds(50));
    }

    public string ToKey( PlaneFrame frame) => $"plane_{frame.Now}";
    public string ToPreAggregateKey( PlaneFrame frame ) => $"plane_preaggregate_{frame.Source}_{frame.Antenna}_{frame.Now}";
}