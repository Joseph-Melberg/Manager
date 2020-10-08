using System;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral
{
    public interface IHeartbeatRepository
    {
        HeartbeatModel[] GetStatuses();
        bool GetState(string name);
        Task UpdateAsync(HeartbeatModel heartBeat);
    }
}
