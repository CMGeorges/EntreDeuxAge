using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQBasicReceiver.Models;

namespace RabbitMQBasicReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        var messageObject = JsonConvert.DeserializeObject<Message>(message);

                        Console.WriteLine(" [x] Received {0}", DateTime.FromBinary(messageObject.TimeStamp).ToShortTimeString() +" " +messageObject.Body.Substring(0,10));
                        
                    };
                    channel.BasicConsume(queue: "entre2ages",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}
