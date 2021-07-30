using System;
using System.Threading.Tasks;
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

        public async Task Process(Heartbeat message)
        {
            //We only need to announce if it was off
            var shouldAnnounce = !await _infraservice.GetHeartbeatStateAsync(message.name);
            message.timestamp = DateTime.Now;
            message.announced = shouldAnnounce;
            message.online = true;
            
            await _infraservice.UpdateAsync(message);
            Console.WriteLine($"{message.name} was proccessed");
        }
    }
}
