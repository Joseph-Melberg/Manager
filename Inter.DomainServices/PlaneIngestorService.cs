using System;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;

public class PlaneIngestorService : IPlaneIngestorService
{
    private readonly IPlaneIngestorInfrastructureService _infrastructure;
    public PlaneIngestorService(IPlaneIngestorInfrastructureService infrastructureService)
    {
        _infrastructure = infrastructureService;
    }

    public async Task HandleMessageAsync(PlaneFrame frame)
    {
        await _infrastructure.IngestPlaneFrameAsync(frame);
        var metadata = new PlaneFrameMetadata();
        metadata.Total = frame.Planes.Count();
        metadata.Detailed = frame.Planes.Where(_ => _.lat.HasValue && _.lon.HasValue).Count();
        

        metadata.Antenna = frame.Antenna;
        metadata.Hostname = frame.Source;
        metadata.Timestamp = DateTime.UnixEpoch.AddSeconds(frame.Now);

        await _infrastructure.UploadPlaneFrameMetadataAsync(metadata);
    }
}