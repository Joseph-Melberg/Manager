using System.Threading;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral;

public interface ICpuUtilizationMarkRepository
{
    Task RecordUsageAsync(CpuUtilization usage, CancellationToken ct);
}