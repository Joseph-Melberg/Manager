using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral
{
    public interface IHeartbeatRepository
    {
        Task<Heartbeat> GetStatusAsync(string name);
        Task<List<Heartbeat>> GetStatiAsync();
        Task UpdateAsync(Heartbeat heartBeat);
    }
}
