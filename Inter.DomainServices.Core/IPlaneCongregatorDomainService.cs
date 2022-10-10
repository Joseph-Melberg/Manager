using System.Threading.Tasks;

namespace Inter.DomainServices.Core;

public interface IPlaneCongregatorDomainService
{
    Task CongregatePlaneInfoAsync(long timestamp);
}