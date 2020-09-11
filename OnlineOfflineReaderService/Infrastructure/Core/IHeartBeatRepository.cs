using System;
namespace OnlineOfflineReaderService.Infrastructure.Core
{
    public interface IHeartBeatRepository
    {
        void Update(string Name, DateTime Timestamp);
    }
}
