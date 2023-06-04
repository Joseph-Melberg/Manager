using System.Threading.Tasks;
using Inter.Domain;
using Inter.Infrastructure.Core;
using Inter.Infrastructure.Corral;

namespace Inter.Infrastructure.Services;

public class MetricsLoggerInfrastructureService : IMetricsLoggerInfrastructureService
{
    private readonly IMetricMarkRepository _repository;

    public MetricsLoggerInfrastructureService(IMetricMarkRepository repository) => 
        _repository = repository;

    public Task RecordMetricAsync(Metric metric) =>
        _repository.MarkMetricAsync( metric);
}