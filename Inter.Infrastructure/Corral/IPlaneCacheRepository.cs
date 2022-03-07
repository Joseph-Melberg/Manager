using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;


public interface IPlaneCacheRepository
{
    Task InsertPlaneFrameAsync(PlaneFrame frame);
    Task InsertCongregatedPlaneFrameAsync(PlaneFrame frame);
    Task<PlaneFrame> GetPlaneFrameAsync(long timestamp);
    Task InsertPreCongregatedPlaneFrameAsync(PlaneFrame planeFrame);
    IAsyncEnumerable<PlaneFrame> GetPreCongregatedPlaneFramesAsync(long timestamp);
}