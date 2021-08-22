using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using Melberg.Infrastructure.Rabbit.Consumers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Inter.LogRecieverAppService.Application
{
    public class LogProcessor : IStandardConsumer
    {
        private readonly ILogListenerService _service;
        public LogProcessor(ILogListenerService service)
        {
            _service = service;
        }

        public async Task ConsumeMessageAsync(string message)
        {
            Console.WriteLine(" [x] Received {0} at {1}", message, DateTime.Now);
            message = message.Replace('\'', '\"');
            try
            {
                var result = JsonSerializer.Deserialize<LogMessage>(message);
                await _service.Process(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
