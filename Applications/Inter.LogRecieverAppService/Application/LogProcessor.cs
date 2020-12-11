using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Inter.Domain;
using Inter.DomainServices.Core;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Inter.LogRecieverAppService.Application
{
    public class LogProcessor
    {
        static string QueueName = "logqueue";
        private readonly ILogListenerService _service;
        public LogProcessor(ILogListenerService service)
        {
            _service = service;
        }
        public async Task Run()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.UserName = "log";
            factory.Password = "oldspice";
            factory.VirtualHost = "/";
            factory.DispatchConsumersAsync = true;
            factory.HostName = "rabbit.centurionx.net";
            factory.ClientProvidedName = "app:LogListener component:event-consumer";
            IConnection connection = factory.CreateConnection();

            var channel = connection.CreateModel();
            channel.ExchangeDeclare("Inter", ExchangeType.Direct, true);
            channel.QueueDeclare(QueueName, false, false, false, null);
            channel.QueueBind(QueueName, "Inter", "/log", null);
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
                System.Threading.Thread.Sleep(1000);
            }
        }
        public async Task HandleAsync(string mess)
        {


            Console.WriteLine(" [x] Received {0} at {1}", mess, DateTime.Now);
            var message = mess.Replace('\'', '\"');
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
