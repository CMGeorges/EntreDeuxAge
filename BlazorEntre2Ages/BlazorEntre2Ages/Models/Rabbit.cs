using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BlazorEntre2Ages.Services;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BlazorEntre2Ages.Models
{
    public class Rabbit : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IMessageService _messageService;

        public Rabbit(IMessageService messageService)
        {
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel(); ;
            _channel.QueueDeclare(queue: "hello",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
            _messageService = messageService;
        }

        public void SendMessage(string message)
        {
            var messageObject = new Message
            {
                Author = Guid.NewGuid(),
                Guest = Guid.NewGuid(),
                TimeStamp = DateTime.Now.Ticks,
                Body = message
            };
            var json = JsonConvert.SerializeObject(messageObject);

            var body = Encoding.UTF8.GetBytes(json);

            _channel.BasicPublish(exchange: "",
                                 routingKey: "hello",
                                 basicProperties: null,
                                 body: body);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Message messageObject = null;

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                messageObject = JsonConvert.DeserializeObject<Message>(message);
                HandleMessage(messageObject);
            };
            _channel.BasicConsume(queue: "entre2ages",
                                 autoAck: true,
                                 consumer: consumer);

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;

            return Task.CompletedTask;
        }

        protected void HandleMessage(Message message)
        {
            _messageService.HandleMessage(message);
        }


        private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }

    }
}
