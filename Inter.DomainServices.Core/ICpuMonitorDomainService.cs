using System.Threading;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core;

public interface ICpuMonitorDomainService
{
    Task RecordAsync(CpuUtilization usage, CancellationToken ct);
}