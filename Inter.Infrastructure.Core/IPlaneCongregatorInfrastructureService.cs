using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;

public interface IPlaneCongregatorInfrastructureService 
{ 
    IAsyncEnumerable<PlaneFrame> CollectPlaneStatesAsync(long timestamp);

    Task UploadCongregatedPlanesAsync(PlaneFrame frame);

    Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata metadata);
}