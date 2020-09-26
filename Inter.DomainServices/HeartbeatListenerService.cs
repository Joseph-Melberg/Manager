using System;
using Inter.DomainServices.Core;

namespace Inter.DomainServices
{
    public class HeartbeatListenerService : IHeartbeatListenerService
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
