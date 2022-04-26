using Melberg.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Mappers;

public static class NodeLifeMarkerMapper
{
    public static InfluxDBDataModel GenerateLifeMarker(string nodeName, bool isAlive)
    {
        if(string.IsNullOrEmpty(nodeName))
        {
            return null;
        }

        var result = new InfluxDBDataModel("node_status");

        result.Tags["name"] = nodeName;
        result.Fields["status"] = isAlive;

        return result;
    }
}