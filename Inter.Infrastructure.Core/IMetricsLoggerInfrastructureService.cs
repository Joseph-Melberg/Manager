using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;
public interface IMetricsLoggerInfrastructureService
{
    Task RecordMetricAsync(Metric metric);
}
