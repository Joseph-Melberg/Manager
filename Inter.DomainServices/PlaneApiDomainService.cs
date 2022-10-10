using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Domain;

namespace Inter.DomainServices;
public class PlaneApiDomainService : IPlaneApiDomainService
{
    private readonly IPlaneApiInfrastructureService _infra;

    public PlaneApiDomainService(IPlaneApiInfrastructureService infrastructureService)
    {
        _infra = infrastructureService;
    }

    public async Task<PlaneFrame> GetFrameAsync(long timestamp) => await _infra.GetFrameAsync(timestamp);
}