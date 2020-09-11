using System;
using OnlineOfflineReaderService.Domain;
using OnlineOfflineReaderService.Infrastructure.Core;

namespace OnlineOfflineReaderService.Infrastructure.MySql
{
    public class HeartBeatRepository : IHeartBeatRepository
    {

        private readonly IHeartBeatContext _heartBeatContext;

        public HeartBeatRepository(IHeartBeatContext heartBeatContext)
        {
            _heartBeatContext = heartBeatContext;
        }

        public void Update(HeartBeatModel heartBeat)
        {
            _heartBeatContext.HeartBeats.Add(heartBeat);
        }
    }
}
