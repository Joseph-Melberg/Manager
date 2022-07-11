using System.Threading;
using System.Threading.Tasks;

namespace Inter.DomainServices.Core;

public interface IMetronomeService
{
    Task StartAsync(CancellationToken cancellationToken);
}