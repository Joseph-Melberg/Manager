using System.Threading;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Consumers;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Translator;

namespace Inter.LifeAlertAppService.Application;

public class Processor : IStandardConsumer
{
    private readonly ILifeAlertDomainService _service;

    public Processor(
        ILifeAlertDomainService service
    )
    {
        _service = service;
    }

    public Task ConsumeMessageAsync(Message message, CancellationToken ct) => _service.Do();
}