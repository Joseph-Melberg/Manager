using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core;

public interface IMetricMonitorDomainService
{
    Task RecordMetricAsync( Metric metric);
}