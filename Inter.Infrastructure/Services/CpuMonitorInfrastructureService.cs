using System.Threading;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;

public class CpuMonitorInfrastructureService : ICpuMonitorInfrastructureService
{
    private readonly ICpuUtilizationMarkRepository _markRepository;
    public CpuMonitorInfrastructureService(
        ICpuUtilizationMarkRepository markRepository
        )
    {
        _markRepository = markRepository;
    }

    public Task RecordAsync(CpuUtilization usage, CancellationToken ct)
    {
        //return Task.CompletedTask;
        return _markRepository.RecordUsageAsync(usage, ct);
    }
}