using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.PlaneListenerService.Models;
using Inter.PlaneListenerService.Mappers;
using Newtonsoft.Json;
using Melberg.Infrastructure.Rabbit.Consumers;
using System;

namespace Inter.PlaneListenerService.Application
{
    public class PlaneProcessor : IStandardConsumer
    {

        private readonly IPlaneListenerService _service;
        public PlaneProcessor(IPlaneListenerService service) => _service = service;
    
        public async Task ConsumeMessageAsync(string message) 
        {
            try
            {
                await _service.HandleMessageAsync(JsonConvert.DeserializeObject<AirplaneRecord>(message).ToDomain());

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}