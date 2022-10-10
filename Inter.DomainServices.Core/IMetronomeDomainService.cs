using System.Threading;
using System.Threading.Tasks;

namespace Inter.DomainServices.Core;

public interface IMetronomeDomainService
{
    Task StartAsync(CancellationToken cancellationToken);
}