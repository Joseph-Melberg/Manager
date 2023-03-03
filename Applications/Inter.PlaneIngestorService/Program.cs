using Melberg.Application;

namespace Inter.PlaneIngestorService;

class Program
{
    static async Task Main(string[] args) => await MelbergHost.CreateDefaultApp<Startup>().Build().Begin(CancellationToken.None);
}