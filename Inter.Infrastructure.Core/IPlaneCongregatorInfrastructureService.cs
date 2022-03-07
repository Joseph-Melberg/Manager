using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;

public interface IPlaneCongregatorInfrastructureService 
{ 
    IAsyncEnumerable<PlaneFrame> CollectFramesAsync(long timestamp);
    Task UploadCongregatedPlanesAsync(PlaneFrame frame);

    Task TrackMetadata(long timestamp, string antenna, int total, int detailed);
}