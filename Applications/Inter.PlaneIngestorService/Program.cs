using Melberg.Application;

namespace Inter.PlaneIngestorService;

class Program
{
    static async Task Main()
    {
        Console.Write("A");
        await MelbergHost.CreateDefaultApp<Startup>().Build().StartAsync();

    }
}
