using MelbergFramework.Application;

namespace Inter.RemoteLogReaderService;

class Program
{
    static async Task Main(string[] args) => await MelbergHost.CreateDefaultApp<Startup>().Build().Begin(CancellationToken.None);
}