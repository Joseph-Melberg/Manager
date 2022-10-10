using Inter.DomainServices.Core;
using Inter.Infrastructure.Rabbit.Messages;
using Melberg.Infrastructure.Rabbit.Consumers;
using Melberg.Infrastructure.Rabbit.Messages;
using Melberg.Infrastructure.Rabbit.Translator;

namespace Inter.PlaneCongregatorService.Application;

public class Processor : IStandardConsumer
{
    private readonly IPlaneCongregatorDomainService _service;
    private readonly IJsonToObjectTranslator<TickMessage> _translator;
    public Processor(
        IPlaneCongregatorDomainService service,
        IJsonToObjectTranslator<TickMessage> translator
    ) 
    { 
        _service = service;
        _translator = translator;    
    }

    public async Task ConsumeMessageAsync(Message message, CancellationToken ct)
    {
        var tickMessage = _translator.Translate(message);

        var timestamp = (long)Math.Floor(tickMessage.Timestamp.Subtract(DateTime.UnixEpoch).TotalSeconds);

        await _service.CongregatePlaneInfoAsync(timestamp);
    }
}