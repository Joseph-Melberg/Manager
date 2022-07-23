using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral;

public interface INodeStateMarkRepository
{
    Task MarkNodeStatusAsync(NodeStatus status);
}