using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;

public class PlaneCongregatorInfrastructureService : IPlaneCongregatorInfrastructureService
{
    private readonly IPlaneCacheRepository _planeCacheRepository;
    public PlaneCongregatorInfrastructureService(IPlaneCacheRepository planeCacheRepository)
    {
        _planeCacheRepository = planeCacheRepository;
    }

    public async IAsyncEnumerable<PlaneFrame> CollectFramesAsync(long timestamp)
    {
        await foreach(var plane in _planeCacheRepository.GetPreCongregatedPlaneFramesAsync(timestamp))
        {
           yield return plane;
        }

        yield break;
    }

    public async Task UploadCongregatedPlanesAsync(PlaneFrame frame) => await _planeCacheRepository.InsertCongregatedPlaneFrameAsync(frame);

    public Task TrackMetadata(long timestamp, string antenna, int total, int detailed)
    {
        throw new System.NotImplementedException();
    }
}