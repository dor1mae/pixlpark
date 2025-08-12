using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqConsumer.RabbitMq
{
    internal class RabbitMqListener
    {
        private string _uri;
        private string _queue;

        private IConnection _connection;
        private IChannel _channel;
        private AsyncEventingBasicConsumer _consumer;

        public RabbitMqListener(string uri, string queue)
        {
            _uri = uri;
            _queue = queue;

            var factory = new ConnectionFactory() { Uri = new Uri(_uri) };

            Initiate(factory);
        }

        private async void Initiate(ConnectionFactory factory)
        {
            await CreateConnection(factory);
            await CreateChannel();

            await _channel.QueueDeclareAsync(queue: "MyQueue",
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);

            CreateConsumer();
        }

        private async Task CreateConnection(ConnectionFactory factory)
        {
            _connection =  await factory.CreateConnectionAsync();
        }

        private async Task CreateChannel()
        {
            _channel = await _connection.CreateChannelAsync();
        }

        private void CreateConsumer()
        {
            _consumer = new AsyncEventingBasicConsumer(_channel);
            _consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            _channel.BasicConsumeAsync(
                queue: _queue,
                autoAck: true,
                consumer: _consumer);
        }

        public void Dispose()
        {
            _channel.CloseAsync();
            _connection.CloseAsync();
        }
    }
}
