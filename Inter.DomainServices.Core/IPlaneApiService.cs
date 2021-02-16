using System.Threading.Tasks;

namespace Inter.DomainServices.Core
{
    public interface IPlaneApiService
    {
        Task<int> CountDetailed();
    }
}