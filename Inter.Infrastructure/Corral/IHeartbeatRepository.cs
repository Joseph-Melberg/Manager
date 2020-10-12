using System;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral
{
    public interface IHeartbeatRepository
    {
        HeartbeatModel[] GetStatuses();
        HeartbeatModel GetStatus(string name);
        Task UpdateAsync(HeartbeatModel heartBeat);
    }
}
