using System;
using Inter.Domain;
using MelbergFramework.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Mappers;

public static class MetricMapper
{
    public static InfluxDBDataModel ToDataModel(this Metric metric)
    {
        var result = new InfluxDBDataModel("service_data");

        result.Measurement = "duration";
        result.Tags["app"] = metric.Application;
        result.Fields["duration"] = metric.TimeInMS * 1000;
        result.Timestamp = metric.TimeStamp;
        var j = DateTime.UtcNow;

        return result;
    }
}