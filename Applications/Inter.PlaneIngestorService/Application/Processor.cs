using System.Diagnostics;
using Inter.DomainServices.Core;
using Inter.PlaneIngestorService.Mappers;
using Inter.PlaneIngestorService.Models;
using Melberg.Infrastructure.Rabbit.Consumers;
using Newtonsoft.Json;

namespace Inter.PlaneIngestorService.Application;

public class Processor : IStandardConsumer
{
    private readonly IPlaneIngestorService _service;
    public Processor(IPlaneIngestorService service) => _service = service;
    public async Task ConsumeMessageAsync(string message)
    {
        Stopwatch timer = new Stopwatch();
        timer.Start();
        Console.WriteLine(" [x] Received frame at {0}", DateTime.Now);
        var commentTime = timer.ElapsedMilliseconds;
        try
        {
            var package = JsonConvert.DeserializeObject<AirplaneRecord>(message)?.ToDomain();
            if(package == null)
            {
                throw new Exception($"Could not process {message}");
            }
            await _service.HandleMessageAsync(package);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        timer.Stop();
        var totalTime = timer.ElapsedMilliseconds;
        Console.WriteLine($"Process took {totalTime-commentTime}");
    }
}