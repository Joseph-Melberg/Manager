using System.Threading.Tasks;

namespace Inter.DomainServices.Core;
public interface IRemoteService
{
    Task<bool> Handle(string name, int port);
}