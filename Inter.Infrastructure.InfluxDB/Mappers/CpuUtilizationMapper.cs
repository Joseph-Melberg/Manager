using System;
using Inter.Domain;
using Melberg.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Mappers;

public static class CpuUtilizationMapper
{
    public static InfluxDBDataModel ToDataModel(this CpuUtilization usage)
    {
        if(usage == null)
        {
            return null;
        }

        var result = new InfluxDBDataModel("node_life");

        result.Tags["hostname"] = usage.Host;
        result.Fields["usage"] = usage.Utilization;
        result.Timestamp = ((DateTimeOffset)usage.TimeStamp).ToUnixTimeSeconds();

        return result;
    }
}