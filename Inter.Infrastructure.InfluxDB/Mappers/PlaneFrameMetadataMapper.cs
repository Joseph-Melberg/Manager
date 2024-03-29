using System;
using Inter.Domain;
using MelbergFramework.Infrastructure.InfluxDB;

namespace Inter.Infrastructure.InfluxDB.Mappers;

public static class PlaneFrameMetadataMapper 
{
    public static InfluxDBDataModel ToDataModel(this PlaneFrameMetadata metadata)
    {
        if(metadata == null)
        {
            return null;
        }

        var result = new InfluxDBDataModel("plane_metadata");

        result.Tags["antenna"] = metadata.Antenna;
        result.Tags["hostname"] = metadata.Hostname;
        result.Fields["total"] = metadata.Total;
        result.Fields["detailed"] = metadata.Detailed;
        result.Timestamp = (long)metadata.Timestamp.Subtract(DateTime.UnixEpoch).TotalSeconds;

        return result;
    }
}