using System;
using OnlineOfflineReaderService.Domain;

namespace OnlineOfflineReaderService.Infrastructure.Core
{
    public interface IHeartBeatRepository
    {
        void Update(HeartBeatModel heartBeat);
    }
}
