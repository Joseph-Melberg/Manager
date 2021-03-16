using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core
{
    public interface IPlaneListenerService
    {
        Task HandleMessageAsync(PlaneFrame Frame);
    }
}