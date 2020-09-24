using System;
using OnlineOfflineReaderService.Domain;

namespace OnlineOfflineReaderService.DomainService.Core
{
    public interface IHeartBeatService
    {
        void Process(HeartBeatMessage message);
    }
}