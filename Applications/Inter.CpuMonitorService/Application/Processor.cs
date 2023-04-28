using Inter.CpuMonitorService.Mappers;
using Inter.CpuMonitorService.Messages;
using Inter.DomainServices.Core;
using MelbergFramework.Infrastructure.Rabbit.Consumers;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Translator;

namespace Inter.CpuMonitorService;

public class Processor : IStandardConsumer
{
    private readonly ICpuMonitorDomainService _service;
    private readonly IJsonToObjectTranslator<CpuUsageMessage> _translator;
    public Processor(
        IJsonToObjectTranslator<CpuUsageMessage> translator,
        ICpuMonitorDomainService service
        )
    {
        _translator = translator;
        _service = service;
    }
    public Task ConsumeMessageAsync(Message message, CancellationToken ct) => _service.RecordAsync(_translator.Translate(message).ToDomain(),ct);
}