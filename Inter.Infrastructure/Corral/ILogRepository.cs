using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral;
public interface ILogRepository
{
    Task AddLog(LogModel logModel);
}