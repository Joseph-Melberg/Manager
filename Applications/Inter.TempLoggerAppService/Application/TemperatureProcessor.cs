using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.TempLoggerAppService.Mappers;
using Inter.TempLoggerAppService.Messages;
using Melberg.Infrastructure.Rabbit.Consumers;
using Melberg.Infrastructure.Rabbit.Messages;
using Melberg.Infrastructure.Rabbit.Translator;

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
        var j = 1;
        _service = service;
    }
    public async Task ConsumeMessageAsync(Message message, CancellationToken ct)
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        var payload = _translator.Translate(message);
        await _service.RecordTempAsync(payload.ToDomain());
        watch.Stop();
        Console.WriteLine($"Process took {watch.ElapsedMilliseconds}");
    }
}