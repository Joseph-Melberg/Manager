using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral
{
    public interface IHeartbeatRepository
    {
        Task<HeartbeatModel> GetStatusAsync(string name);
        Task<List<HeartbeatModel>> GetStatuses();
        Task UpdateAsync(HeartbeatModel heartBeat);
    }
}
