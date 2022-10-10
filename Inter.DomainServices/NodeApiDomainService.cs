using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;
public class NodeApiDomainService : INodeApiDomainService
{
    private readonly INodeApiInfrastructureService _infra;

    public NodeApiDomainService(INodeApiInfrastructureService infra)
    {
        _infra = infra;
    }

    public async Task<int> GetUpCountAsync() => (await _infra.GetStatiAsync()).Where(_ => _.online).Count();

    public async Task<IList<Heartbeat>> GetListAsync() => await _infra.GetStatiAsync();
}