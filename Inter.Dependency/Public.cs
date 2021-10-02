using Inter.DomainServices;
using Inter.DomainServices.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.Dependency
{
    public static partial class Dependency
    {
        public static IServiceCollection RegisterPlaneListenerService(this IServiceCollection collection)
        {
            collection.AddTransient<IPlaneListenerService,PlaneListenerService>();

            
            return collection.RegisterPlaneListenerInfrastructureService();
        }

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

        public static IServiceCollection RegisterTemperatureListenerService(this IServiceCollection collection)
        {
            collection.AddTransient<ITemperatureListenerService,TemperatureListenerService>();

            collection.RegisterTemperatureListenerInfrastructureService(); 
            
            return collection;
        }
    }
}