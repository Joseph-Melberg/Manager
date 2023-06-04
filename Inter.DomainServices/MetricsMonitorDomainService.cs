using System;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices;

public class MetricMonitorDomainService : IMetricMonitorDomainService
{
    private readonly IMetricMonitorInfrastructureService _infraService;
    public MetricMonitorDomainService(
        IMetricMonitorInfrastructureService infraService
        )
    {
        _infraService = infraService;
    }
    public Task RecordMetricAsync(Metric metric) 
    {
        if(string.IsNullOrEmpty(metric.Application))
        {
            Console.Write("This metric has no name and will not be processed");
            return Task.CompletedTask;
        }

        return _infraService.RecordMetricAsync( metric);
    }
}