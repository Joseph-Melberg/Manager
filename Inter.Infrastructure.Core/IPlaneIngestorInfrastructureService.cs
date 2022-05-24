using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;

public interface IPlaneIngestorInfrastructureService
{
    Task IngestPlaneFrameAsync(PlaneFrame planeFrame);
    Task UploadPlaneFrameMetadataAsync(PlaneFrameMetadata metadata);
    Task<PlaneFrame> GetPlaneSourceStateAsync(PlaneSourceDefintion source);

    Task SetPlaneSourceStateAsync(PlaneSourceDefintion source, PlaneFrame data);
    Task SetPlaneSourceDeltaAsync(PlaneSourceDefintion source, PlaneFrameDelta data);
}