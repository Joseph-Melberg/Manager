using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core
{
    public interface ILogListenerInfrastructureService
    {
        Task AddLog(LogModel  logModel);
    }
}
