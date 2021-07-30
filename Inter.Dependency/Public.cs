using Inter.DomainServices;
using Inter.DomainServices.Core;
using Microsoft.Extensions.DependencyInjection;
namespace Inter.Dependency
{
    public static partial class Dependency
    {
        public static IServiceCollection RegisterNodeApiService(this IServiceCollection Collection)
        {
            Collection.AddTransient<INodeApiService, NodeApiService>();

            Collection.RegisterNodeApiInfrastructureService();

            return Collection;
        }

        public static IServiceCollection RegisterHeartbeatListenerService(this IServiceCollection Collection)
        {
            Collection.AddTransient<IHeartbeatListenerService, HeartbeatListenerService>();
            Collection.RegisterHearbeatListenerInfrastructureService();
            return Collection;
        }
    }
}