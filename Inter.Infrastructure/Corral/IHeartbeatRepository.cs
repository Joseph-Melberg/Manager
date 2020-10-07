using System;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral
{
    public interface IHeartbeatRepository
    {
        bool GetState(string name);
        Task UpdateAsync(HeartbeatModel heartBeat);
    }
}
