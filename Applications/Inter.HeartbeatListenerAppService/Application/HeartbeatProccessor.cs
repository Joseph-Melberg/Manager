using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
namespace Inter.HeartbeatListenerAppService.Application
{
    public class HeartbeatProccessor
    {

        static string QueueName = "Reader";
        private readonly IHeartbeatListenerService _service;
        public HeartbeatProccessor(IHeartbeatListenerService service)
        {
            _service = service;
        }
        public async Task Run()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "life";
            factory.Password = "conway";
            factory.VirtualHost = "/";
            factory.DispatchConsumersAsync = true;
            factory.HostName = "centurionx.net";
            factory.ClientProvidedName = "app:audit component:event-consumer";
            IConnection connection = factory.CreateConnection();

            var channel = connection.CreateModel();
            channel.ExchangeDeclare("Inter", ExchangeType.Direct, true);
            channel.QueueDeclare(QueueName, false, false, false, null);
            channel.QueueBind(QueueName, "Inter", "/life", null);
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
                var result = JsonSerializer.Deserialize<HeartbeatMessage>(mess);
                await _service.Process(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("That didn't work");

            }
        }
    }
}
