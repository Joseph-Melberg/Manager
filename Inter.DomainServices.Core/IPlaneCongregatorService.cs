using System.Threading.Tasks;

namespace Inter.DomainServices.Core;

public interface IPlaneCongregatorService
{
    Task CongregatePlaneInfoAsync(long timestamp);
}