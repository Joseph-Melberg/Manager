using System;
using Inter.DomainServices.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.LifeAlertAppService
{
    class Program
    {

        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            RegisterServices();
            Console.WriteLine("Hello World!");
            

            _serviceProvider.GetRequiredService<ILifeAlertService>().Do();


            DisposeServices();
        }

        private static void RegisterServices()
        {

            var services = new ServiceCollection();
            Register.RegisterServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
