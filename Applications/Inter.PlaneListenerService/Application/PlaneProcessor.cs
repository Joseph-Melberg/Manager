using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.PlaneListenerService.Models;
using Inter.PlaneListenerService.Mappers;
using Newtonsoft.Json;
using Melberg.Infrastructure.Rabbit.Consumers;
using System;
using System.Diagnostics;

namespace Inter.PlaneListenerService.Application
{
    public class PlaneProcessor : IStandardConsumer
    {

        private readonly IPlaneListenerService _service;
        public PlaneProcessor(IPlaneListenerService service) => _service = service;
    
        public async Task ConsumeMessageAsync(string message) 
        {

            Stopwatch timer = new Stopwatch();
            timer.Start();
            Console.WriteLine(" [x] Received frame at {0}", DateTime.Now);
            var commentTime = timer.ElapsedMilliseconds;
            try
            {
                var package = JsonConvert.DeserializeObject<AirplaneRecord>(message).ToDomain();
                await _service.HandleMessageAsync(package);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            timer.Stop();
            var totalTime = timer.ElapsedMilliseconds;
            Console.WriteLine($"{commentTime}:{totalTime-commentTime}");
        }
    }
}