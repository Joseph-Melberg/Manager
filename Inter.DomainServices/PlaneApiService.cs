using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices
{
    public class PlaneApiService : IPlaneApiService
    {
        private readonly IPlaneApiInfrastructureService _infra;

        public PlaneApiService(IPlaneApiInfrastructureService infrastructureService)
        {
            _infra = infrastructureService;
        }
        public async Task<int> CountDetailed()
        {
            var result = await _infra.GetPlaneCount();
            return result;
        }
    }
}