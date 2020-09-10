using System;
using OnlineOfflineReaderService.Domain;

namespace OnlineOfflineReaderService.DomainService.Core
{
    public interface IOnlineOfflineReaderService
    {
        void Process(HeartBeatMessage message);
    }
}