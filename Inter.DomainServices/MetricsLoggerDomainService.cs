using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;

public class MetricsLoggerDomainService : IMetricsLoggerDomainService
{
    private readonly IMetricsLoggerInfrastructureService _infraService;
    public MetricsLoggerDomainService(IMetricsLoggerInfrastructureService infraService ) =>
        _infraService = infraService;
    public Task RecordMetricAsync(Metric metric) => 
        _infraService.RecordMetricAsync( metric);
}