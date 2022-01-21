using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Melberg.Infrastructure.Rabbit.Consumers;

namespace Inter.HeartbeatListenerAppService.Application;

public class HeartbeatProcessor : IStandardConsumer
{

    private readonly IHeartbeatListenerService _service;
    public HeartbeatProcessor(IHeartbeatListenerService service)
    {
        _service = service;
    }
    public async Task ConsumeMessageAsync(string message)
    {
        Console.WriteLine(" [x] Received {0} at {1}", message, DateTime.Now);
        Stopwatch watch = new Stopwatch();
        watch.Start();
        message = message.Replace("'", "\"");
        try
        {
            var result = JsonSerializer.Deserialize<HeartbeatMessage>(message);
            await _service.Process(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("That didn't work");
        }
        watch.Stop();
        Console.WriteLine($"Process took {watch.ElapsedMilliseconds}");
    }
}
