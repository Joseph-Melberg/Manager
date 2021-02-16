using System.Threading.Tasks;

namespace Inter.Infrastructure.Corral
{
    public interface IPlaneRepository
    {
        Task<int> PlaneCount(); 
    }
}