using System.Linq;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices
{
    public class NodeStatusService : INodeStatusService
    {
        private readonly INodeStatusInfrastructureService _infrastructure;
        public NodeStatusService(INodeStatusInfrastructureService infrastructureService)
        {
            _infrastructure = infrastructureService;
        }

        public async Task<int> GetUpCount()
        {
            return (await _infrastructure.GetStati()).Where(_ => _.online).Count();
        }
    }
}
