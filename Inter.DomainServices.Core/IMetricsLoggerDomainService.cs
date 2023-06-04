using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core;

public interface IMetricsLoggerDomainService
{
    Task RecordMetricAsync( Metric metric);
}