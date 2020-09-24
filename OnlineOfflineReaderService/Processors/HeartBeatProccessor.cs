using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using OnlineOfflineReaderService.Domain;
using OnlineOfflineReaderService.DomainService.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OnlineOfflineReaderService.Processors
{
    public class HeartBeatProccessor
    {

        static string QueueName = "Reader";
        private readonly IHeartBeatService _service;
        public HeartBeatProccessor(IHeartBeatService service)
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
            channel.ExchangeDeclare("Inter", ExchangeType.Fanout, true);
            channel.QueueDeclare(QueueName, false, false, false, null);
            channel.QueueBind(QueueName, "Inter", "/life", null);
            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.Received += async (ch, ea) =>
            {

                channel.BasicAck(ea.DeliveryTag, false);
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Handle(message);
                await Task.Yield();

            };
            String consumerTag = channel.BasicConsume(QueueName, false, consumer);
            while (true)
            {
                await Task.Yield();
            }
        }
        public void Handle(string mess)
        {


            Console.WriteLine(" [x] Received {0}", mess);
            mess = mess.Replace("'","\"");
            try
            {
                var result = JsonSerializer.Deserialize<HeartBeatMessage>(mess);
                _service.Process(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("That didn't work");
                
            }
            
        }
    }
}
