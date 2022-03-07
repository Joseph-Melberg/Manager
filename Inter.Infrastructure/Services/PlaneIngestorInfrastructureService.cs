using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class PlaneIngestorInfrastructureService : IPlaneIngestorInfrastructureService
    {
        private readonly IPlaneCacheRepository _planeCacheRepository;
        public PlaneIngestorInfrastructureService(IPlaneCacheRepository planeCacheRepository)
        {
            _planeCacheRepository = planeCacheRepository;
        }

        public async Task IngestPlaneFrameAsync(PlaneFrame planeFrame) => await _planeCacheRepository.InsertPreCongregatedPlaneFrameAsync(planeFrame);
    }
}