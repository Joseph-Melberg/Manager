using Inter.DomainServices;
using Inter.DomainServices.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.Dependency
{
    public static partial class Dependency
    {
        public static IServiceCollection RegisterNodeControllerService(this IServiceCollection collection)
        {
            collection.AddTransient<INodeApiService, NodeApiService>();

            collection.RegisterNodeApiInfrastructureService(); 
            return collection;
        }

        public static IServiceCollection RegisterHeartbeatListenerService(this IServiceCollection collection)
        {

            collection.AddTransient<IHeartbeatListenerService, HeartbeatListenerService>();

            collection.RegisterHeartbeatListenerInfrastructureService();

            return collection;
        }

        public static IServiceCollection RegisterLogListenerService(this IServiceCollection collection)
        {
            collection.AddTransient<ILogListenerService, LogListenerService>();

            collection.RegisterLogListenerInfrastructureService();

            return collection;
        }
    }
}