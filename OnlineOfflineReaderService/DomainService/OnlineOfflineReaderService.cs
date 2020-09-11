using System;
using OnlineOfflineReaderService.Domain;
using OnlineOfflineReaderService.DomainService.Core;

namespace OnlineOfflineReaderService.DomainService
{
    public class OnlineOfflineReaderService : IOnlineOfflineReaderService
    {
        public OnlineOfflineReaderService()
        {
        }

        public void Process(HeartBeatMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
