using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core
{
    public interface ILifeAlertInfrastructureService
    {
        Task<IList<Heartbeat>> GetStatusesAsync();
        Task UpdateNode(Heartbeat model);
        void SendMessage(string recipient, string subject, string message);
    }
}
