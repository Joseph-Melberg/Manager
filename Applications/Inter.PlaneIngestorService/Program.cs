using MelbergFramework.Application;

namespace Inter.PlaneIngestorService;

class Program
{
    static async Task Main()
    {
        await MelbergHost.CreateDefaultApp<Startup>().Build().Begin(CancellationToken.None);
    }
}
