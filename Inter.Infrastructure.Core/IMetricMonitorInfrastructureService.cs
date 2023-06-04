using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;
public interface IMetricMonitorInfrastructureService
{
    Task RecordMetricAsync(Metric metric);
}
