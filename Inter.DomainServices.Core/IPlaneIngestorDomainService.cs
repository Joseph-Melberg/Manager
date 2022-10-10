using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core;

public interface IPlaneIngestorDomainService
{
    Task HandleMessageAsync(PlaneFrame frame);
}