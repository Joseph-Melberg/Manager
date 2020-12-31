using System.Threading.Tasks;

namespace Inter.Infrastructure.Core
{
    public interface IPlaneApiInfrastructureService
    {
        Task<int> GetPlaneCount();
    }
}