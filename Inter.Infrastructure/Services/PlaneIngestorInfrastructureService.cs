using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services
{
    public class PlaneIngestorInfrastructureService : IPlaneIngestorInfrastructureService
    {
        private readonly IPlaneCacheRepository _planeCacheRepository;
        private readonly IPlaneFrameMetadataRepository _influxPlaneMetadataRepository;
        public PlaneIngestorInfrastructureService(
            IPlaneCacheRepository planeCacheRepository,
            IPlaneFrameMetadataRepository planeFrameMetadataRepository)
        {
            _planeCacheRepository = planeCacheRepository;
            _influxPlaneMetadataRepository = planeFrameMetadataRepository;
        }

        public async Task IngestPlaneFrameAsync(PlaneFrame planeFrame) => await _planeCacheRepository.InsertPreCongregatedPlaneFrameAsync(planeFrame);

        public async Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata metadata) => await _influxPlaneMetadataRepository.LogPlaneMetadata(metadata);
    }
}