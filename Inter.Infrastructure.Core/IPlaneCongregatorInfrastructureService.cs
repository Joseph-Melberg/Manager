using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;

public interface IPlaneCongregatorInfrastructureService 
{ 
    Task<IEnumerable<PlaneFrameDelta>> CollectDeltaFramesAsync(long timestamp);
    Task<IEnumerable<Plane>> CollectPlaneStatesAsync();
    Task UploadPlaneStates();
    Task UploadCongregatedPlanesAsync(PlaneFrame frame);

    Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata metadata);
}