using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Rabbit.Messages;
using Melberg.Infrastructure.Rabbit.Consumers;
using Melberg.Infrastructure.Rabbit.Messages;
using Melberg.Infrastructure.Rabbit.Translator;

namespace Inter.LifeAlertAppService.Application;

public class Processor : IStandardConsumer
{
    private readonly ILifeAlertService _service;

    public Processor(
        ILifeAlertService service
    )
    {
        _service = service;
    }

    public Task ConsumeMessageAsync(Message message) => _service.Do();
}