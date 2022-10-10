using Inter.DomainServices;
using Inter.DomainServices.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.Dependency;
public static partial class Dependency
{

    public static IServiceCollection RegisterNodeControllerService(this IServiceCollection collection)
    {
        collection.AddTransient<INodeApiDomainService, NodeApiDomainService>();

        collection.RegisterNodeApiInfrastructureService(); 

        return collection;
    }
    
    public static IServiceCollection RegisterPlaneControllerService(this IServiceCollection collection)
    {
        collection.AddTransient<IPlaneApiDomainService,PlaneApiDomainService>();

        collection.RegisterPlaneApiInfrastructureService();

        return collection;
    }

}