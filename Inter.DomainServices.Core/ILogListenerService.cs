using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.DomainServices.Core
{
    public interface ILogListenerService
    {
        Task Process(LogMessage message);
    }
}