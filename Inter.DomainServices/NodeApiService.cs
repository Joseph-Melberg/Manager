using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices
{
    public class NodeApiService : INodeApiService
    {
        private readonly INodeApiInfrastructureService _infrastructure;
        public NodeApiService(INodeApiInfrastructureService infrastructureService)
        {
            _infrastructure = infrastructureService;
        }

        public async Task<int> GetUpCountAsync()
        {
            return (await _infrastructure.GetStatiAsync()).Where(_ => _.online).Count();
        }

        public async Task<IList<Heartbeat>> GetStatiAsync() => await _infrastructure.GetStatiAsync(); 
    }
}
