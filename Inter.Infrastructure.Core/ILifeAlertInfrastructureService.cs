using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Core;
public interface ILifeAlertInfrastructureService
{
    Task<List<Heartbeat>> GetStatusesAsync();
    Task MarkStateAsync(NodeStatus status);
    Task UpdateNodeAsync(Heartbeat model);
}