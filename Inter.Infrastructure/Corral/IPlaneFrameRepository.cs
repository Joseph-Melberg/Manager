using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral;
public interface IPlaneFrameRepository
{
    Task InsertFrameAsync(PlaneFrame frame);
    
    Task<PlaneFrame> GetFrameAsync(long time);
}