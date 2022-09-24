using System.Linq;
using Inter.Domain;
using InterApi.ServiceModels;

namespace InterApi.Mappers;

public static class PlaneResponseMapper
{
    public static PlaneFrameResponse ToResponse(this PlaneFrame model)
    {
        if(model == null)
            return null;
        
        return new PlaneFrameResponse
        {
            Now = model.Now,
            Antenna = model.Antenna,
            Source = model.Source,
            Planes = model.Planes.Select(_ => _.ToResponse()).ToArray()
        };
    }
    public static PlaneResponse ToResponse(this Plane model)
    {
        if(model == null)
            return null;
        
        return new PlaneResponse
        {
            Altitude = model.Altitude,
            Category = model.Category,
            Flight = model.Flight,
            HexValue = model.HexValue,
            Latitude = model.Latitude,
            Longitude = model.Longitude,
            Messages = model.Messages,
            Nucp = model.Nucp,
            Rssi = model.Rssi,
            Speed = model.Speed,
            Squawk = model.Squawk,
            Track = model.Track,
            VerticleRate = model.VerticleRate
        };
    }
}