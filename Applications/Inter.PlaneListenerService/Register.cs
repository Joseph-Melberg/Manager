using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.DomainServices;
using Microsoft.Extensions.DependencyInjection;
using Inter.Infrastructure.Services;
using Inter.Infrastructure.Corral;
using Inter.Infrastructure.MySQL.Repositories;
using Inter.Infrastructure.MySQL.Contexts;
using Melberg.Infrastructure.MySql;
using Melberg.Infrastructure.Couchbase;
using Inter.PlaneListenerService.Application;
using Inter.Infrastructure.Couchbase;

namespace Inter.PlaneListenerService
{
    public class Register
    {
        public static ServiceCollection RegisterServices(ServiceCollection services)
        {
            services.AddSingleton<PlaneProcessor>();
            services.AddTransient<IPlaneListenerService, DomainServices.PlaneListenerService>();
            services.AddTransient<IPlaneListenerInfrastructureService,
                PlaneListenerInfrastructureService>();
            CouchbaseModule.RegisterCouchbaseClient<IPlaneFrameRepository,PlaneFrameRepository>(services);
            return services;
        }
    }
}

    