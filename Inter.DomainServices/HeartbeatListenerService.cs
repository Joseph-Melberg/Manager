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

        public async Task Process(HeartbeatMessage message)
        {
            //We only need to announce if it was off
            var shouldAnnounce = !await _infraservice.GetHeartbeatStateAsync(message.Name);
            var model = new HeartbeatModel()
            {
                name = message.Name,
                mac = message.Mac,
                timestamp = DateTime.Now,
                announced = shouldAnnounce,
                online = true
            };
            await _infraservice.UpdateAsync(model);
            Console.WriteLine($"{message.Name} was proccessed");
        }
    }
}
