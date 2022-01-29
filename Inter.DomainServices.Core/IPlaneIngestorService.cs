using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core;

public interface IPlaneIngestorService
{
    Task HandleMessageAsync(PlaneFrame frame);
}