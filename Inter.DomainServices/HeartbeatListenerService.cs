using System;
using Inter.Domain;
using Inter.DomainServices.Core;
using Inter.Infrastructure.Core;

namespace Inter.DomainServices
{
    public class HeartbeatListenerService : IHeartbeatListenerService
    {
        private readonly IHeartbeatListenerInfrastructureService _infraservice;
        public HeartbeatListenerService(IHeartbeatListenerInfrastructureService infrastructureService)
        {
            _infraservice = infrastructureService;
        }

        public void Process(HeartbeatMessage message)
        {
            var model = new HeartbeatModel()
            {
                name = message.Name,
                mac = message.Mac,
                timestamp = DateTime.Now
            };
            _infraservice.UpdateAsync(model);
        }
    }
}
