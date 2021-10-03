using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Domain;

namespace Inter.DomainServices
{
    public class PlaneApiService : IPlaneApiService
    {
        private readonly IPlaneApiInfrastructureService _infra;

        public PlaneApiService(IPlaneApiInfrastructureService infrastructureService)
        {
            _infra = infrastructureService;
        }

        public Task<PlaneFrame> GetFrameAsync(long timestamp) => _infra.GetFrameAsync(timestamp);
    }
}