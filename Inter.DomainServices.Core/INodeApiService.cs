using System.Threading.Tasks;

namespace Inter.DomainServices.Core
{
    public interface INodeStatusService
    {
        Task<int> GetUpCount();
    }
}