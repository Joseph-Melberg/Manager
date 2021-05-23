using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Inter.DomainServices.Core;
using Inter.PlaneListenerService.Models;
using Inter.PlaneListenerService.Mappers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Newtonsoft.Json;

namespace Inter.PlaneListenerService.Application
{
    public class PlaneProcessor
    {

        private readonly IPlaneListenerService _service;
        public PlaneProcessor(IPlaneListenerService service) => _service = service;
        private readonly string QueueName = "plane";
    
        public async Task Run()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "planey"; 
            factory.Password = "mcplaneyface";
            factory.VirtualHost = "/";
            factory.DispatchConsumersAsync = true;
            factory.HostName = "rabbit.centurionx.net";
            factory.ClientProvidedName = "app:audit component:event-consumer";
            IConnection connection = factory.CreateConnection();

            var channel = connection.CreateModel();
            channel.ExchangeDeclare("Inter", ExchangeType.Direct, true);
            channel.QueueDeclare(QueueName, false, false, false, null);
            channel.QueueBind(QueueName, "Inter", "/plane", null);
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += async (ch, ea) =>
            {

                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await HandleAsync(message);

                channel.BasicAck(ea.DeliveryTag, false);
                await Task.Yield();

            };
            String consumerTag = channel.BasicConsume(QueueName, false, consumer);
            while (true)
            {
                //The next line reduces the cpu usage from 60% down below 10%
                await Task.Delay(Timeout.Infinite);
            }
        }
        public async Task HandleAsync(string mess)
        {
            Console.WriteLine(" [x] Received {0} at {1}", mess, DateTime.Now);
            mess = mess.Replace("'", "\"");
            try
            {
                Console.WriteLine("Begin Serialization");
                var result = JsonConvert.DeserializeObject<AirplaneRecord>(mess);

                var check = JsonConvert.SerializeObject(result);
                Console.WriteLine("Serialized");
                Console.WriteLine(check);
                await _service.HandleMessageAsync(result.ToDomain());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine("That didn't work");
            }
        }
    }
}