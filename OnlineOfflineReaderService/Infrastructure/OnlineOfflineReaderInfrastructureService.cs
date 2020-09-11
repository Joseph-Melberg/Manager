using System;
using OnlineOfflineReaderService.Infrastructure.Core;

namespace OnlineOfflineReaderService.Infrastructure
{
    public class OnlineOfflineReaderInfrastructureService : IOnlineOfflineReaderInfrastructureService
    {
        public OnlineOfflineReaderInfrastructureService(IHeartBeatContext heartBeatContext)
        {
            _heartBeatContext = heartBeatContext;
        }


        public void Update(string Name, DateTime timestamp)
        {
            _heartBeatContext.
        }
    }
}
