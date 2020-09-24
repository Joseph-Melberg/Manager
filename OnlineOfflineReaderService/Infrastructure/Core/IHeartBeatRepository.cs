using System;
using System.Threading.Tasks;
using OnlineOfflineReaderService.Domain;

namespace OnlineOfflineReaderService.Infrastructure.Core
{
    public interface IHeartBeatRepository
    {
        Task UpdateAsync(HeartBeatModel heartBeat);
    }
}
