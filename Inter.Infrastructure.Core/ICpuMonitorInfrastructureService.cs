using System.Threading;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;

public interface ICpuMonitorInfrastructureService
{
    Task RecordAsync(CpuUtilization usage, CancellationToken ct);
}