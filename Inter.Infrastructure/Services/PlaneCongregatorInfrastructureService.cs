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

    public async Task<IEnumerable<PlaneFrameDelta>> CollectDeltaFramesAsync(long timestamp)
    {
        var result = new List<PlaneFrameDelta>();
        await foreach(var planeFrameDelta in _planeCacheRepository.GetPlaneSourceDeltasAsync(timestamp))
        {
            result.Add(planeFrameDelta);
        }
        return result;
    }

    public Task<IEnumerable<Plane>> CollectPlaneStatesAsync()
    {
        throw new System.NotImplementedException();
    }

    public async Task UploadCongregatedPlanesAsync(PlaneFrame frame) => await _planeCacheRepository.InsertCongregatedPlaneFrameAsync(frame);

    public async Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata metadata) => await _influxPlaneMetadataRepository.LogPlaneMetadata(metadata);

    public async Task UploadPlaneStates(IEnumerable<Plane> planes)
    {
        await Task.WhenAll(planes.Select(_ => _planeCacheRepository.UpdatePlaneRecordAsync(_)));
    }
}