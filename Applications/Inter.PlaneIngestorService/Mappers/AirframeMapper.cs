
using System;
using System.Collections.Generic;
using Inter.Domain;
using Inter.PlaneIngestorService.Models;

namespace Inter.PlaneIngestorService.Mappers;
public static class AirFrameMapper
{
    public static PlaneFrame ToDomain(this AirplaneRecord record)
    {
        if(record == null) return null;

        var result = new PlaneFrame();

        result.Now = (long)record.Now;
        result.Antenna = record.Antenna;
        result.Source = record.Source;

        var resultingPlanes = new List<Plane>();
        
        foreach(var plane in record.Planes)
        {
            if(plane.IsValid())
            {
                resultingPlanes.Add(plane.ToDomain());
            }
        }

        result.Planes = resultingPlanes.ToArray();

        return result;
    }

    private static bool IsValid(this AirplaneData data) =>
        data.lat.HasValue &&
        data.lon.HasValue &&
        data.altitude.HasValue &&
        data.vert_rate.HasValue &&
        data.track.HasValue &&
        data.speed.HasValue &&
        data.rssi.HasValue
    ;
}