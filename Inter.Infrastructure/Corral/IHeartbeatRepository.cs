using System;
using System.Threading.Tasks;
using Inter.Domain;

namespace Inter.Infrastructure.Corral
{
    public interface IHeartbeatRepository
    {
        Task UpdateAsync(HeartbeatModel heartBeat);
    }
}
