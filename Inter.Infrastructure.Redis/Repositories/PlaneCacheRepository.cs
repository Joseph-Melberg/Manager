using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.Redis.Contexts;
using Melberg.Infrastructure.Redis.Repository;
using Newtonsoft.Json;

namespace Inter.Infrastructure.Redis.Repositories
{
    public class PlaneCacheRepository : RedisRepository<PlaneCacheContext>, IPlaneCacheRepository
    {
        public PlaneCacheRepository(PlaneCacheContext context) : base(context)
        {
        }

        public async Task InsertPlaneFrameAsync(PlaneFrame frame)
        {
            var key = $"plane_{frame.Now}";
            var payload = JsonConvert.SerializeObject(frame.Planes);
            await DB.StringSetAsync(key,payload,new System.TimeSpan(0,0,45));
        }
    }
}