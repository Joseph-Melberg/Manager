using Inter.Domain;
using Inter.PlaneIngestorService.Models;

namespace Inter.PlaneIngestorService.Mappers;
public static class PlaneMapper
{
    public static TimeAnotatedPlane ToDomain(this AirplaneData data)
    {
       if(data == null) return null;
       return new TimeAnotatedPlane
       {
            Altitude = data.altitude ?? null,
            AltitudeUpdated = data.altitude_update,
            Category = data.category,
            CategoryUpdated = data.category_update,
            Flight = data.flight,
            FlightUpdated = data.flight_update,
            HexValue = data.hex,
            Latitude = data.lat ?? null,
            Longitude = data.lon ?? null,
            PositionUpdated = data.position_update,
            Messages = data.messages,
            Nucp = data.nucp,
            Rssi = data.rssi ?? null,
            Speed = data.speed ?? null,
            SpeedUpdated = data.speed_update,
            Squawk = data.squawk,
            SquawkUpdated = data.squawk_update,
            Track = data.track ?? null,
            TrackUpdated = data.track_update,
            VerticleRate = data.vert_rate ?? null,
            VerticleRateUpdated = data.vert_update
       };
    }
}