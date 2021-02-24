using System;
using System.Text;
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
                    TimeStamp = DateTime.Now.Ticks,
                    Body = "In sadipscing nonumy sed accusam magna et dolore consequat duis et erat justo magna nulla voluptua consetetur vero lorem elitr stet duo elit consequat sadipscing labore diam blandit nisl dolore sea dolor erat takimata dolor lorem sed kasd lobortis stet autem est gubergren dolor velit invidunt facilisis sed sed duis"
                };
                var json = JsonConvert.SerializeObject(messageObject);
                
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", json);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
