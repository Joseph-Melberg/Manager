namespace Inter.Domain;

public class TimeAnotatedPlane : Plane
{
    public ulong? SquawkUpdated {get; set;}
    public ulong? FlightUpdated {get; set;}
    public ulong? PositionUpdated {get; set;}
    public ulong? AltitudeUpdated {get; set;}
    public ulong? VerticleRateUpdated {get; set;}
    public ulong? TrackUpdated {get; set;}
    public ulong? SpeedUpdated {get; set;}
    public ulong? CategoryUpdated {get; set;}
}