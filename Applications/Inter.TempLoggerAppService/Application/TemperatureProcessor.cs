using System;
using System.Text.Json;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.TempLoggerAppService.Mappers;
using Inter.TempLoggerAppService.Messages;
using Melberg.Infrastructure.Rabbit.Consumers;

namespace Inter.TempLoggerAppService.Application;
public class TemperatureProcessor : IStandardConsumer
{
    private readonly ITemperatureListenerService _service;
    public TemperatureProcessor(ITemperatureListenerService service)
    {
        _service = service;
    }
    public async Task ConsumeMessageAsync(string message)
    {
        Console.WriteLine(" [x] Received {0} at {1}", message, DateTime.Now);
        
        message = message.Replace("'", "\"");
        try
        {
            var result = JsonSerializer.Deserialize<TemperatureMessage>(message);
            await _service.RecordTempAsync(result.ToDomain());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}