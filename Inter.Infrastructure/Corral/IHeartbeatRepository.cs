using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral
{
    public interface IHeartbeatRepository
    {
        Task<Heartbeat> GetStatusAsync(string name);
        Task<IList<Heartbeat>> GetStatusesAsync();
        Task UpdateAsync(Heartbeat heartBeat);
    }
}
