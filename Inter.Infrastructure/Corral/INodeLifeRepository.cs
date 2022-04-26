using System.Threading.Tasks;

namespace Inter.Infrastructure.Corral;

public interface INodeLifeRepository
{
    Task MarkStatusAsync(string nodeName, bool isAlive);
}