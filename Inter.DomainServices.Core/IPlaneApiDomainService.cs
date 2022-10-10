using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core;
public interface IPlaneApiDomainService
{
    Task<PlaneFrame> GetFrameAsync(long timestamp);
}