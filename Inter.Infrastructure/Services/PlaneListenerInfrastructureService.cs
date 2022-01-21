using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;
public class PlaneListenerInfrastructureService : IPlaneListenerInfrastructureService
{
    private readonly IPlaneCacheRepository _planeCacheRepository;
    private readonly IPlaneFrameMetadataRepository _planeFrameMetadataRepository;
    
    public PlaneListenerInfrastructureService(
        IPlaneCacheRepository planeCacheRepository,
        IPlaneFrameMetadataRepository planeFrameMetadataRepository)
    {
        _planeCacheRepository = planeCacheRepository;
        _planeFrameMetadataRepository = planeFrameMetadataRepository;
    }

    public async Task AddPlaneFrameAsync(PlaneFrame frame)
    {
        await _planeCacheRepository.InsertPlaneFrameAsync(frame);
    }
    public async Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata metadata)
    {
        await _planeFrameMetadataRepository.UploadPlaneFrameMetadataAsync(metadata);
    }
}
