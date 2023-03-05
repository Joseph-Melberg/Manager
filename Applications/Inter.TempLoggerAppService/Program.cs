using System;
using System.Threading;
using System.Threading.Tasks;
using Melberg.Application;
namespace Inter.TempLoggerAppService;

class Program
{
    static async Task Main(string[] args) 
    {
        
	    await MelbergHost.CreateDefaultApp<Startup>().Build().Begin(CancellationToken.None);
    }
}
