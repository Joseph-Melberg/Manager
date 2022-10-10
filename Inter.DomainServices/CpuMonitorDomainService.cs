using System.Threading;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;

public class CpuMonitorDomainService : ICpuMonitorDomainService
{
    private readonly ICpuMonitorInfrastructureService _infra;
    public CpuMonitorDomainService(ICpuMonitorInfrastructureService infra)
    {
        _infra = infra;
    }
    public Task RecordAsync(CpuUtilization usage, CancellationToken ct)
    {
        return _infra.RecordAsync(usage,ct);
    }
}