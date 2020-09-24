using System;
using OnlineOfflineReaderService.Domain;
using OnlineOfflineReaderService.DomainService.Core;
using OnlineOfflineReaderService.Infrastructure.Core;

namespace OnlineOfflineReaderService.DomainService
{
    public class HeartBeatService : IHeartBeatService
    {
        private readonly IHeartBeatInfrastructureService _infraservice;
        public HeartBeatService(IHeartBeatInfrastructureService infrastructureService)
        {
            _infraservice = infrastructureService;
        }

        public void Process(HeartBeatMessage message)
        {
            var model = new HeartBeatModel()
            {
                name = message.Name,
                mac = message.Mac,
                timestamp = DateTime.Now
            };
             _infraservice.UpdateAsync(model);
        }
    }
}
