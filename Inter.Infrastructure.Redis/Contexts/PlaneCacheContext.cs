using Melberg.Core.Redis;
using Melberg.Infrastructure.Redis;

namespace Inter.Infrastructure.Redis.Contexts
{
    public class PlaneCacheContext : RedisContext
    {
        public PlaneCacheContext(IRedisConfigurationProvider provider) : base(provider)
        {
        }
    }
}