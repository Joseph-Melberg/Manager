using System;
using System.Threading.Tasks;
using OnlineOfflineReaderService.Domain;
using OnlineOfflineReaderService.Infrastructure.Core;

namespace OnlineOfflineReaderService.Infrastructure
{
    public class HeartBeatInfrastructureService : IHeartBeatInfrastructureService
    {
        private readonly IHeartBeatRepository _heartBeatRepository;
        public HeartBeatInfrastructureService(IHeartBeatRepository heartBeatRepository)
        {
            _heartBeatRepository = heartBeatRepository;
        }


        public async Task UpdateAsync(HeartBeatModel heartBeat)
        {
            await _heartBeatRepository.UpdateAsync(heartBeat);
        }
    }
}