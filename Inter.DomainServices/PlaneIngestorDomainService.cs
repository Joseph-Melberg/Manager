using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;

public class PlaneIngestorDomainService : IPlaneIngestorDomainService
{
    private readonly IPlaneIngestorInfrastructureService _infrastructure;
    public PlaneIngestorDomainService(IPlaneIngestorInfrastructureService infrastructureService)
    {
        _infrastructure = infrastructureService;
    }

    public async Task HandleMessageAsync(PlaneFrame frame)
    {
        var insertTask = _infrastructure.IngestPlaneFrameAsync(frame);

        var metadataTask = HandleMetadata(frame);

        await Task.WhenAll(insertTask,metadataTask);
    }

    private async Task HandleMetadata(PlaneFrame frame)
    {
        var metadata = new PlaneFrameMetadata();
        metadata.Total = frame.Planes.Count();
        metadata.Detailed = frame.Planes.Where(_ => _.Latitude.HasValue && _.Longitude.HasValue).Count();

        metadata.Antenna = frame.Antenna;
        metadata.Hostname = frame.Source;
        metadata.Timestamp = DateTime.UnixEpoch.AddSeconds(frame.Now);

        await _infrastructure.UploadPlaneFrameMetadataAsync(metadata);
    }
}