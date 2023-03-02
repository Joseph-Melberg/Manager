using System.Threading;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Melberg.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.TempLoggerAppService;

class Program
{
    //static void Main()
    //{
        //var services = MelbergHost.CreateDefaultApp<Startup>().Build().Services;
        //var mark = services.GetService<ITemperatureMarkRepository>();
        
    //}
    static async Task Main(string[] args) => await  MelbergHost.CreateDefaultApp<Startup>().Build().Begin(CancellationToken.None);
}