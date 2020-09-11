using System;
using OnlineOfflineReaderService.Domain;
using OnlineOfflineReaderService.Infrastructure.Core;

namespace OnlineOfflineReaderService.Infrastructure
{
    public class OnlineOfflineReaderInfrastructureService : IOnlineOfflineReaderInfrastructureService
    {
        private readonly IHeartBeatRepository _heartBeatRepository;
        public OnlineOfflineReaderInfrastructureService(IHeartBeatRepository heartBeatRepository)
        {
            _heartBeatRepository = heartBeatRepository;
        }


        public void Update(HeartBeatModel heartBeat)
        {
            _heartBeatRepository.Update(heartBeat)
        }
    }
}
