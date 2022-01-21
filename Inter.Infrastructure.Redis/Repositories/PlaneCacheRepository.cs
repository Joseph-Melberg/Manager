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
        var planes = payload.ToDomain((int)timestamp);
        return planes; 
    }

    public Task InsertPlaneFrameAsync(PlaneFrame frame) => DB.StringSetAsync(frame.ToKey(),frame.ToPayload(),new TimeSpan(0,0,45));
}