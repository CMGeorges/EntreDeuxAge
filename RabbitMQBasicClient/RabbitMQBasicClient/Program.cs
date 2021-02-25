using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQBasicClient.Models;

namespace RabbitMQBasicClient
{
    class Program
    {
        static void Main(string[] args)
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

                var messageObject = new Message
                {
                    Author = Guid.NewGuid(),
                    Guest = Guid.NewGuid(),
                    Body = "Pauvre dans flanc monstre ton femme de d'athlete masque ronge la n'est genoux l'elégance voici",
                    Mine = true
                };

                while (true)
                {
                    messageObject.Mine = !messageObject.Mine;
                    messageObject.TimeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    var json = JsonConvert.SerializeObject(messageObject);
                    
                    var body = Encoding.UTF8.GetBytes(json);
                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent {0}", json);
                    Thread.Sleep(5000);

                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
