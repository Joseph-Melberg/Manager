using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.PlaneListenerService.Models;
using Inter.PlaneListenerService.Mappers;
using Newtonsoft.Json;
using Melberg.Infrastructure.Rabbit.Consumers;

namespace Inter.PlaneListenerService.Application
{
    public class PlaneProcessor : IStandardConsumer
    {

        private readonly IPlaneListenerService _service;
        public PlaneProcessor(IPlaneListenerService service) => _service = service;
    
        public Task ConsumeMessageAsync(string message) => _service.HandleMessageAsync(JsonConvert.DeserializeObject<AirplaneRecord>(message).ToDomain());

    }
}