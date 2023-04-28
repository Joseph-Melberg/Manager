using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.TempLoggerAppService.Mappers;
using Inter.TempLoggerAppService.Messages;
using MelbergFramework.Infrastructure.Rabbit.Consumers;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Translator;

namespace Inter.TempLoggerAppService.Application;
public class TemperatureProcessor : IStandardConsumer
{
    private readonly ITemperatureListenerDomainService _service;
    private readonly IJsonToObjectTranslator<TemperatureMessage> _translator;
    public TemperatureProcessor(
        ITemperatureListenerDomainService service,
        IJsonToObjectTranslator<TemperatureMessage> translator)
    {
        _translator = translator;
        _service = service;
    }
    public async Task ConsumeMessageAsync(Message message, CancellationToken ct)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        var payload = _translator.Translate(message);
        try
        {
            await _service.RecordTempAsync(payload.ToDomain());
            
        }
        catch (System.Exception ex)
        {
            
            throw;
        }
        watch.Stop();
        Console.WriteLine($"Process took {watch.ElapsedMilliseconds}");
    }
}
