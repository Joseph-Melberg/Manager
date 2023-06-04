using Inter.DomainServices.Core;
using Inter.MetricsLoggerAppService.Mappers;
using MelbergFramework.Infrastructure.Rabbit.Consumers;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Metrics;
using MelbergFramework.Infrastructure.Rabbit.Translator;

namespace Inter.MetricsLoggerAppService.Application;

public class Processor : IStandardConsumer
{
    private readonly IJsonToObjectTranslator<MetricMessage> _translator;
    private readonly IMetricsLoggerDomainService _service;
    public Processor(
        IJsonToObjectTranslator<MetricMessage> translator,
        IMetricsLoggerDomainService service)
    {
        _service = service;
        _translator = translator;
    }

    public Task ConsumeMessageAsync(Message message, CancellationToken ct) => 
        _service.RecordMetricAsync(_translator.Translate(message).ToDomain());

}