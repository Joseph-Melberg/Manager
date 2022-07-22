using Inter.Common.Clock;
using Inter.Common.Configuration;
using Inter.Common.Configuration.Providers;
using Inter.DomainServices;
using Inter.DomainServices.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.Dependency;
public static partial class Dependency
{
    public static IServiceCollection RegisterPlaneCongregatorService(this IServiceCollection collection)
    {
        collection.AddTransient<IPlaneCongregatorService,PlaneCongregatorService>();

        return collection
                .RegisterPlaneCongregatorInfrastructureService();
    }

    public static IServiceCollection RegisterPlaneIngestorService(this IServiceCollection collection)
    {
        collection.AddTransient<IPlaneIngestorService,PlaneIngestorService>();

        return collection.RegisterPlaneIngestorInfrastructureService();
    }

    public static IServiceCollection RegisterMetronomeService(this IServiceCollection collection)
    {
       collection.AddTransient<IMetronomeService,MetronomeService>();


       return collection
            .RegisterClock()
            .RegisterMetronomeInfrastructureService(); 
    }

    public static IServiceCollection RegisterLifeAlertService(this IServiceCollection collection)
    {
        collection.AddTransient<ILifeAlertService,LifeAlertService>();
        collection.AddSingleton<ILifeAlertRateConfiguration,LifeAlertRateConfigurationProvider>();
        collection.AddSingleton<IEmailRecipientConfiguration,EmailRecipientConfigurationProvider>();

        return collection.RegisterLifeAlertInfrastructureService();
    }

    public static IServiceCollection RegisterNodeControllerService(this IServiceCollection collection)
    {
        collection.AddTransient<INodeApiService, NodeApiService>();

        collection.RegisterNodeApiInfrastructureService(); 

        return collection;
    }
    
    public static IServiceCollection RegisterPlaneControllerService(this IServiceCollection collection)
    {
        collection.AddTransient<IPlaneApiService,PlaneApiService>();

        collection.RegisterPlaneApiInfrastructureService();

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
    #region Misc

    public static IServiceCollection RegisterClock(this IServiceCollection collection)
    {
        collection.AddTransient<IClock,Clock>();

        return collection;
    }

    #endregion Misc
}