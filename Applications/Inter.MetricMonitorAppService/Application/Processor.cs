using Inter.DomainServices.Core;
using Inter.MetricMonitorAppService.Mappers;
using MelbergFramework.Infrastructure.Rabbit.Consumers;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Metrics;
using MelbergFramework.Infrastructure.Rabbit.Translator;

namespace Inter.MetricMonitorAppService.Application;

public class Processor : IStandardConsumer
{
    private readonly IJsonToObjectTranslator<MetricMessage> _translator;
    private readonly IMetricMonitorDomainService _service;
    public Processor(
        IJsonToObjectTranslator<MetricMessage> translator,
        IMetricMonitorDomainService service)
    {
        _service = service;
        _translator = translator;
    }

    public async Task ConsumeMessageAsync(Message message, CancellationToken ct) 
    {
        var mess = _translator.Translate(message).ToDomain();
        Console.WriteLine($"{mess.TimeStamp}");
        await _service.RecordMetricAsync(mess);
    } 


}