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

    public async Task HandleMessageAsync(PlaneFrame frame) => await _infrastructure.IngestPlaneFrameAsync(frame);
}