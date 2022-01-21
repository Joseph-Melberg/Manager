using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral;
public interface IPlaneCacheRepository
{
    Task InsertPlaneFrameAsync(PlaneFrame frame);

    Task<PlaneFrame> GetPlaneFrameAsync(long timestamp);
}