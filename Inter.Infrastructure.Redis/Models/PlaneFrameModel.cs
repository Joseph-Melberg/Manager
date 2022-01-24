using Inter.Domain;

namespace Inter.Infrastructure.Redis.Models;

public class PlaneFrameModel
{
    public long Now {get; set;}
    public Plane[] Planes {get; set;}
}