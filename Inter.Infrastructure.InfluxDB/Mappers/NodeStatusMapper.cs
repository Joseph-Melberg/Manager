using System;
using Inter.Domain;
using Melberg.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Mappers;

public static class NodeStatusMapper
{
    public static InfluxDBDataModel ToDataModel(this NodeStatus status)
    {
        if(status == null)
        {
            return null;
        }

        var result = new InfluxDBDataModel("node_life");

        result.Tags["hostname"] = status.Name;
        result.Fields["status"] = (status.Online) ? 1 : 0;
        result.Timestamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();

        return result;
    }
}