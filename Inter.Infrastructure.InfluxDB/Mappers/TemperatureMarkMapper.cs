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

	var result = new InfluxDBDataModel("node_data");

	result.Tags["hostname"] = mark.HostName;
    result.Measurement = "temperature";
	result.Fields["temperature"] = mark.Temperature;
	result.Tags["part"] = mark.PartName;
        result.Timestamp = ((DateTimeOffset)mark.Timestamp).ToUnixTimeSeconds();

	return result;
    }
}
