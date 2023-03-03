using System.Threading;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Melberg.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Inter.TempLoggerAppService;

class Program
{
    static async Task Main(string[] args) => await  MelbergHost.CreateDefaultApp<Startup>().Build().Begin(CancellationToken.None);
}