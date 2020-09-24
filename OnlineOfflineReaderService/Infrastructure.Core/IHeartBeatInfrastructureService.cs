using System;
using System.Threading.Tasks;
using OnlineOfflineReaderService.Domain;

namespace OnlineOfflineReaderService.Infrastructure.Core
{
    public interface IHeartBeatInfrastructureService
    {
        Task UpdateAsync(HeartBeatModel heartBeat);
    }
}
