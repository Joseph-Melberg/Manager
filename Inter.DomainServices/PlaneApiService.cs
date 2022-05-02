using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;
using Inter.Domain;

namespace Inter.DomainServices;
public class PlaneApiService : IPlaneApiService
{
    private readonly IPlaneApiInfrastructureService _infra;

    public PlaneApiService(IPlaneApiInfrastructureService infrastructureService)
    {
        _infra = infrastructureService;
    }

    public async Task<PlaneFrame> GetFrameAsync(long timestamp) => await _infra.GetFrameAsync(timestamp);

    public async Task<PlaneFrame> GetFrameByDeviceAsync(string source, string antenna, long timestamp) =>
        await _infra.GetPreaggregateFrameAsync(source, antenna, timestamp);
}