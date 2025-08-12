using RabbitMQ.Client;
using System.Text;
using System.Text.Json;


public class RabbitMqService : IRabbitMqService
{
    public void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        SendMessage(message);
    }
    
    public async void SendMessage(string message)
	{
		var factory = new ConnectionFactory() { Uri = new Uri("amqps://suqdmlrq:o6BEparo_nOCTMYmbIS69n16Drr-SwJc@kangaroo.rmq.cloudamqp.com/suqdmlrq") };
        var connection = await factory.CreateConnectionAsync();
		using (var channel = await connection.CreateChannelAsync())
		{
			await channel.QueueDeclareAsync(queue: "MyQueue",
                           durable: false,
                           exclusive: false,
                           autoDelete: false,
                           arguments: null);

			var body = Encoding.UTF8.GetBytes(message);

			await channel.BasicPublishAsync(exchange: "",
                           routingKey: "MyQueue",
                           body: body);
		}
	}
}
