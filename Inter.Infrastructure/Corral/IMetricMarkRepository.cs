using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral;

public interface IMetricMarkRepository
{
    Task MarkMetricAsync( Metric metric);
}