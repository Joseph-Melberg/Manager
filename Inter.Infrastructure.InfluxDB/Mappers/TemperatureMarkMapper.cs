using System;
using Inter.Domain;
using Melberg.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Mappers;
public static class TemperatureMarkMapper
{
    public static InfluxDBDataModel ToDataModel(this TemperatureMark mark)
    {
        if(mark == null)
        {
            return null;
        }

        var result = new InfluxDBDataModel("node_temperature");

        result.Tags["hostname"] = mark.HostName;
        result.Fields["part"] = mark.PartName;
        result.Fields["temp"] = mark.Temperature;
        result.Timestamp = ((DateTimeOffset)mark.Timestamp).ToUnixTimeSeconds();

        return result;
    }
}