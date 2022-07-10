using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;

public class PlaneCongregatorInfrastructureService : IPlaneCongregatorInfrastructureService
{
    private readonly IPlaneCacheRepository _planeCacheRepository;
    private readonly IPlaneFrameMetadataRepository _influxPlaneMetadataRepository;
    public PlaneCongregatorInfrastructureService(
        IPlaneCacheRepository planeCacheRepository,
        IPlaneFrameMetadataRepository planeFrameMetadataRepository)
    {
        _planeCacheRepository = planeCacheRepository;
        _influxPlaneMetadataRepository = planeFrameMetadataRepository;
    }

    public IAsyncEnumerable<PlaneFrame> CollectPlaneStatesAsync(long timestamp) => 
        _planeCacheRepository.GetPreCongregatedPlaneFramesAsync(timestamp);

    public async Task UploadCongregatedPlanesAsync(PlaneFrame frame) => await _planeCacheRepository.InsertCongregatedPlaneFrameAsync(frame);

    public async Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata metadata) => await _influxPlaneMetadataRepository.LogPlaneMetadata(metadata);

    public async Task UploadPlaneStates(IEnumerable<Plane> planes)
    {
        await Task.WhenAll(planes.Select(_ => _planeCacheRepository.UpdatePlaneRecordAsync(_)));
    }
}