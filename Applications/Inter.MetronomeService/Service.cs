using System.Threading;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Microsoft.Extensions.Hosting;

namespace Inter.MetronomeService;

public class MetronomeApplicationService : IHostedService
{
    private readonly IMetronomeDomainService _service;
    public MetronomeApplicationService(IMetronomeDomainService service)
    {
        _service = service;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        return _service.StartAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}