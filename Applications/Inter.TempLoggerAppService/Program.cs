using System;
using System.Threading.Tasks;
using Melberg.Application;
namespace Inter.TempLoggerAppService;

class Program
{
    static async Task Main(string[] args) 
    {
        
	var host = MelbergHost.CreateDefaultApp<Startup>().Build();
        await host.StartAsync();
    }
}
