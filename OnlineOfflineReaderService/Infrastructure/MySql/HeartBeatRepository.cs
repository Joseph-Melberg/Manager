using System;
using OnlineOfflineReaderService.Infrastructure.Core;

namespace OnlineOfflineReaderService.Infrastructure.MySql
{
    public class HeartBeatRepository : IHeartBeatRepository
    {

        private readonly IHeartBeatContext _heartBeatContext;

        public HeartBeatRepository()
        {
        }

        public void Update(string Name, DateTime Timestamp)
        {
            throw new NotImplementedException();
        }
    }
}
