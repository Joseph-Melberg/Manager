using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core;
public interface IPlaneApiService
{
    Task<PlaneFrame> GetFrameAsync(long timestamp);
    Task<PlaneFrame> GetFrameByDeviceAsync(string source, string antenna, long timestamp);
}