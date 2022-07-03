namespace Inter.Domain;
public class PlaneFrame
{
    public long Now {get; set;}
    public TimeAnotatedPlane[] Planes {get; set;}
    public string Antenna {get; set;}
    public string Source {get; set;}
}