using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

public interface IPlaneCacheRepository
{
    Task InsertPlaneFrameAsync(PlaneFrame frame);
    Task InsertCongregatedPlaneFrameAsync(PlaneFrame frame);
    Task InsertPreCongregatedPlaneFrameAsync(PlaneFrame planeFrame);
    Task UpdatePlaneRecordAsync(Plane plane);
    Task SetPlaneSourceState(PlaneSourceDefintion source, PlaneFrame frame);
    Task SetPlaneSourceDelta(PlaneSourceDefintion source, PlaneFrameDelta frame);
    Task<PlaneFrame> GetPlaneFrameAsync(long timestamp);
    Task<PlaneFrame> GetPreCongregatedPlaneFrameAsync(PlaneSourceDefintion source, long timestamp);
    Task<PlaneFrame> GetPlaneSourceState(PlaneSourceDefintion source);
    IAsyncEnumerable<PlaneFrame> GetPlaneSourceDeltasAsync(long timestamp);
    IAsyncEnumerable<PlaneFrame> GetPreCongregatedPlaneFramesAsync(long timestamp);
}