using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;
public interface ILifeAlertInfrastructureService
{
    Task<List<Heartbeat>> GetStatusesAsync();
    Task UpdateNodeAsync(Heartbeat model);
    void SendMessage(string recipient, string subject, string message);
}