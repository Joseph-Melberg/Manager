using Inter.Domain;
using MelbergFramework.Infrastructure.Rabbit.Metrics;

namespace Inter.MetricsLoggerAppService.Mappers;

public static class MetricMapper
{
    public static Metric ToDomain(this MetricMessage message) =>
        message == null ? 
            new Metric() :
            new Metric() 
                {
                    Application = message.Application,
                    TimeInMS = message.TimeInMS,
                    TimeStamp = message.TimeStamp
                };
}