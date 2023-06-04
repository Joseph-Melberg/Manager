using System;
using Inter.Domain;
using MelbergFramework.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Mappers;

public static class MetricMapper
{
    public static InfluxDBDataModel ToDataModel(this Metric metric)
    {
        var result = new InfluxDBDataModel("service_data");

        result.Tags["application"] = metric.Application;
        result.Fields["duration"] = metric.TimeInMS;
        result.Timestamp = ((DateTimeOffset)metric.TimeStamp).ToUnixTimeSeconds();

        return result;
    }
}